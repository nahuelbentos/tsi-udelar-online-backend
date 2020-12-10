using System;
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
using Persistence;

namespace Business.AlumnoCursos
{
    public class ExportarPDF
    {
         public class Ejecuta : IRequest<Stream> {   

            public Guid CursoId { get; set; }
            
         }

    public class Manejador : IRequestHandler<Ejecuta, Stream>
    {
       private readonly UdelarOnlineContext context;

      public Manejador(UdelarOnlineContext context)
      {
        this.context = context;
      }

      public async Task<Stream> Handle(Ejecuta request, CancellationToken cancellationToken)
      {
         var alumnoCursos = await this.context.AlumnoCurso
                                                .Include(ac => ac.Alumno)
                                                .Include(ac => ac.Curso)
                                                .Where(ac => ac.CursoId == request.CursoId)
                                                .ToListAsync();

        if (alumnoCursos == null)
            throw new ManejadorExcepcion(HttpStatusCode.BadRequest, new { mensaje = "No se encontraron inscripciones asociadas al Curso" });

        Font fuenteTitulo = new Font(Font.HELVETICA, 8f, Font.BOLD, BaseColor.Blue);
        Font fuenteHeader = new Font(Font.HELVETICA, 7f, Font.BOLD, BaseColor.Black);
        Font fuenteData = new Font(Font.HELVETICA, 6f, Font.NORMAL, BaseColor.Black);

        MemoryStream workStream = new MemoryStream();
        Rectangle rectangle = new Rectangle(PageSize.A4);

        Document document = new Document(rectangle, 0, 0, 50, 100);

        PdfWriter pdfWriter = PdfWriter.GetInstance(document, workStream);

        pdfWriter.CloseStream = false;

        document.Open();
        document.AddTitle("Listado de Alumnos por Curso");
        PdfPTable table = new PdfPTable(1);
        table.WidthPercentage = 90;
        PdfPCell cell = new PdfPCell(new Phrase("Curso "+ alumnoCursos.First().Curso.Nombre, fuenteTitulo));
        cell.Border = Rectangle.NO_BORDER;

        table.AddCell(cell);

        document.Add(table);


        PdfPTable tableCursos = new PdfPTable(2);
        float[] widths = new float[] { 40, 60 };
        tableCursos.SetWidthPercentage(widths, rectangle);

        PdfPCell cellHeaderTitulo = new PdfPCell(new Phrase("Nombre", fuenteHeader));
        tableCursos.AddCell(cellHeaderTitulo);
        PdfPCell cellHeaderDescripcion = new PdfPCell(new Phrase("Mail", fuenteHeader));
        tableCursos.AddCell(cellHeaderDescripcion);
        tableCursos.WidthPercentage = 90;

        foreach (var ac in alumnoCursos)
        {
          PdfPCell cellDataTitulo = new PdfPCell(new Phrase(ac.Alumno.Nombres + ac.Alumno.Apellidos, fuenteData));
          tableCursos.AddCell(cellDataTitulo);
          PdfPCell cellDataDescripcion = new PdfPCell(new Phrase(ac.Alumno.Email, fuenteData));
          tableCursos.AddCell(cellDataDescripcion);
        }

        document.Add(tableCursos);

        document.Close();

        byte[] byteData = workStream.ToArray();

        workStream.Write(byteData, 0, byteData.Length);
        workStream.Position = 0;

        return workStream;

      }
    }
    }
}