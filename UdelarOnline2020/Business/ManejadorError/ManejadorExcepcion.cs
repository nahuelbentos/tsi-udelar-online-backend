using System.Net;
using System;
namespace Business.ManejadorError
{
  public class ManejadorExcepcion : Exception
  {
    public HttpStatusCode Code { get; }
    public object Errores { get; }

    public ManejadorExcepcion(HttpStatusCode code, object errores = null)
    {
      this.Code = code;
      this.Errores = errores;
    }
  }
}