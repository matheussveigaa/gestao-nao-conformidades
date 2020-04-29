using GestaoDeNaoConformidades.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoDeNaoConformidades.Infrastructure.Data
{
    public class GestaoNaoConformidadesDbContext : DbContext
    {
        public GestaoNaoConformidadesDbContext(DbContextOptions<GestaoNaoConformidadesDbContext> options)
            : base(options)
        { }

        public DbSet<NaoConformidade> NaoConformidades { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<AcaoCorretiva> AcaoCorretivas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NaoConformidadeDepartamento>()
                .HasKey(nfd => new { nfd.NaoConformidadeID, nfd.DepartamentoID });
        }
    }
}
