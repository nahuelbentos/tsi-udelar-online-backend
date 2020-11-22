using System;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IBedeliasGenerator
    {
         Task<bool> AprobarInscripcionEvaluacion(string CI, Guid CursoId);
         Task<bool> AprobarInscripcionCurso(string CI, Guid CursoId);
         Task<bool> CerrarActa(string[] CI, Guid CursoId);
    }
}
