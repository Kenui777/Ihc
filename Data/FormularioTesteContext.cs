using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormularioTeste.Models;
using Microsoft.EntityFrameworkCore;

namespace FormularioTeste.Data
{
    public class FormularioTesteContext : DbContext
    {
        public FormularioTesteContext (DbContextOptions<FormularioTesteContext> options)
            : base(options)
        {
        }

        public DbSet<Denuncia> Denuncia { get; set; } = default!;
        public DbSet<Estabelecimento> Estabelecimentos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estabelecimento>()
                .HasKey(
                    p => new { p.CnpjBasico, p.CnpjOrdem, p.CnpjDv });

        }
    }
}
