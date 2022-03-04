using Microsoft.EntityFrameworkCore;
using SmartCity.Domain.Models.Cidades;
using SmartCity.Domain.Models.Ocorrencias;
using SmartCity.Domain.Models.Usuarios;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartCity.Data.Context
{
    public class SmartCityContext : DbContext
    {
        public SmartCityContext(DbContextOptions options) : base(options) { }

        public DbSet<Ocorrencia> Ocorrencias { get; set; }
        public DbSet<OcorrenciaImagem> OcorrenciaImagens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<VotoOcorrencia> OcorrenciaVotos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<MotivoDenuncia> MotivosDenuncia { get; set; }

        public virtual void Commit()
        {
            base.SaveChanges();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ocorrencia>();
            modelBuilder.Entity<OcorrenciaImagem>();
            modelBuilder.Entity<VotoOcorrencia>();
            modelBuilder.Entity<Usuario>();
            modelBuilder.Entity<Cidade>();
            modelBuilder.Entity<MotivoDenuncia>();
            base.OnModelCreating(modelBuilder);


        }

    }
}
