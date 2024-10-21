using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Validacao_Form.Models;

namespace FormularioTeste.Data
{
    public class FormularioTesteContext : DbContext
    {
        public FormularioTesteContext (DbContextOptions<FormularioTesteContext> options)
            : base(options)
        {
        }

        public DbSet<Validacao_Form.Models.Denuncia> Denuncia { get; set; } = default!;
    }
}
