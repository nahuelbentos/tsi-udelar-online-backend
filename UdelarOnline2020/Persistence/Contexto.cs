﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using Models;

namespace Persistence
{
  public class Contexto : IdentityDbContext<Usuario>
  {
        public Contexto() : base()
        {
        }
        public Contexto(DbContextOptions options) : base(options) { }
        public DbSet<Facultad> Facultad { get; set; }
        public DbSet<Curso> Curso { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
  }
}