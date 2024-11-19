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
        public FormularioTesteContext(DbContextOptions<FormularioTesteContext> options)
            : base(options)
        {
        }

        public DbSet<Denuncia> Denuncia { get; set; } = default!;
        public DbSet<Estabelecimento> Estabelecimentos { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Estabelecimento>()
                .HasKey(p => new { p.CnpjBasico, p.CnpjOrdem, p.CnpjDv });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=FormularioTesteContext-55837d85-046f-430f-b029-f714387a192d;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True",
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(30),
                        errorNumbersToAdd: null);
                });
        }
    }
}
