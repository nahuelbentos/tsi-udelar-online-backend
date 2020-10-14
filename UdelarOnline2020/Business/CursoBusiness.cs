using System;
using Models;
using Persistence;

namespace Business
{
    public class CursoBusiness
    {
        private readonly Contexto _context;

        public CursoBusiness()
        {
            _context = new Contexto();
        }

        public Curso GetCurso(int id)
        {
            return _context.Curso.Find(id);
        }
    }
}
