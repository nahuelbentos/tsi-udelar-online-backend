using System;
using Models;
using Persistence;

namespace Business
{
  public class CursoBusiness
  {
    private readonly UdelarOnlineContext context;

    public CursoBusiness(UdelarOnlineContext context)
    {
      this.context = context;
    }

    public Curso GetCurso(int id)
    {
      return context.Curso.Find(id);
    }
  }
}
