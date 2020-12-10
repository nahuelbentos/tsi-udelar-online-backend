using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Business.ManejadorError;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Models;
using Persistence;

namespace Business.Cursos {
  public class ExportarPDF {
    public class Ejecuta : IRequest<Stream> {

      public Guid FacultadId { get; set; }

    }

    public class Manejador : IRequestHandler<Ejecuta, Stream> {
      private readonly UdelarOnlineContext context;

      public Manejador (UdelarOnlineContext context) {
        this.context = context;
      }

      public async Task<Stream> Handle (Ejecuta request, CancellationToken cancellationToken) {
        var facultad = await this.context.Facultad.Include (f => f.CarreraLista).Where (f => f.FacultadId == request.FacultadId).FirstOrDefaultAsync ();

        if (facultad == null)
          throw new ManejadorExcepcion (HttpStatusCode.NotFound, new { mensaje = "No existe una facultad con el Id ingresado" });

        List<Curso> cursos = new List<Curso> ();
        foreach (var carrera in facultad.CarreraLista) {
          var auxCursos = await this.context.CarreraCurso
            .Where (cc => cc.CarreraId == carrera.CarreraId)
            .Select (c => c.Curso)
            .ToListAsync ();
          cursos = cursos.Concat (auxCursos).ToList ();
        }

        Font fuenteTitulo = new Font (Font.HELVETICA, 8f, Font.BOLD, BaseColor.Blue);
        Font fuenteHeader = new Font (Font.HELVETICA, 7f, Font.BOLD, BaseColor.Black);
        Font fuenteData = new Font (Font.HELVETICA, 6f, Font.NORMAL, BaseColor.Black);

        MemoryStream workStream = new MemoryStream ();
        Rectangle rectangle = new Rectangle (PageSize.A4);

        Document document = new Document (rectangle, 0, 0, 50, 100);

        PdfWriter pdfWriter = PdfWriter.GetInstance (document, workStream);

        pdfWriter.CloseStream = false;

        document.Open ();
        document.AddTitle ("Listado de Cursos por Facultad");
        PdfPTable table = new PdfPTable (1);
        table.WidthPercentage = 90;
        PdfPCell cell = new PdfPCell (new Phrase ("Facultad " + facultad.Nombre, fuenteTitulo));
        cell.Border = Rectangle.NO_BORDER;

        table.AddCell (cell);

        document.Add (table);

        PdfPTable tableCursos = new PdfPTable (2);
        float[] widths = new float[] { 40, 60 };
        tableCursos.SetWidthPercentage (widths, rectangle);

        PdfPCell cellHeaderTitulo = new PdfPCell (new Phrase ("Nombre", fuenteHeader));
        tableCursos.AddCell (cellHeaderTitulo);
        PdfPCell cellHeaderDescripcion = new PdfPCell (new Phrase ("Mail", fuenteHeader));
        tableCursos.AddCell (cellHeaderDescripcion);
        tableCursos.WidthPercentage = 90;

        foreach (var c in cursos) {
          PdfPCell cellDataTitulo = new PdfPCell (new Phrase (c.Nombre, fuenteData));
          tableCursos.AddCell (cellDataTitulo);
          PdfPCell cellDataDescripcion = new PdfPCell (new Phrase (c.Descripcion, fuenteData));
          tableCursos.AddCell (cellDataDescripcion);
        }

        document.Add (tableCursos);

        document.Close ();

        byte[] byteData = workStream.ToArray ();

        workStream.Write (byteData, 0, byteData.Length);
        workStream.Position = 0;

        return workStream;

      }
    }
  }
}