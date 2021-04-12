using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Models;

namespace WebApiContabilidadSystem.Data
{
    public class ContabilidadDbContext : DbContext
    {
        public ContabilidadDbContext( DbContextOptions<ContabilidadDbContext> options)
            : base(options)
        { }
        public DbSet<USUARIO> Usuario { get; set; }
        public DbSet<CONCEPTO_PAGO> ConceptoPago { get; set; }
        public DbSet<PROVEEDOR> Proveedor { get; set; }
        public DbSet<ENTRADA_DOCUMENTO> EntradaDocumento { get; set; }
        public DbSet<TIPO_DOCUMENTO> TIPO_DOCUMENTO { get; set; }
    }
}
