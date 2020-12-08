using System;
using System.Net.Http;
using System.Threading.Tasks;
using Business.Interfaces;

namespace Perifericos.Bedelias
{
  public class BedeliasGenerator : IBedeliasGenerator
  {
    HttpClient client = new HttpClient();
    string baseUrl = "http://10.1.4.14:6000/api/bedelias";
    // http://node789-api-udelaronline.web.elasticloud.uy:11015/api
    public async Task<bool> AprobarInscripcionCurso(string CI, Guid CursoId) 
    => await this.readResponse( await this.requestBedelias($"{baseUrl}/aprobar-inscripcion-curso", new { ci = CI, cursoId = CursoId } ));
    public async Task<bool> AprobarInscripcionEvaluacion(string CI, Guid CursoId) 
    => await this.readResponse( await this.requestBedelias($"{baseUrl}/aprobar-inscripcion-evaluacion", new { ci = CI, cursoId = CursoId } ));

    public async Task<bool> CerrarActa(string[] colCI, Guid CursoId)
    => await this.readResponse(await this.requestBedelias($"{baseUrl}/cerrar-acta", new { coleccionCI = colCI, cursoId = CursoId }));

    private async Task<HttpResponseMessage> requestBedelias (string url, object body) => await this.client.PostAsJsonAsync(url, body);
    private async Task<bool> readResponse (HttpResponseMessage response) 
            => (response.IsSuccessStatusCode) ? await response.Content.ReadAsAsync<bool>() : false;
  }
}