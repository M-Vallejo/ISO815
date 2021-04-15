using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Models;

namespace WebApiContabilidadSystem.Data
{
    public class SQLServerDbContext : DbContext, IDataBaseContext
    {
        public SQLServerDbContext( DbContextOptions<SQLServerDbContext> options)
            : base(options)
        { }
        public DbSet<USUARIO> Usuario { get; set; }
        public DbSet<CONCEPTO_PAGO> ConceptoPago { get; set; }
        public DbSet<PROVEEDOR> Proveedor { get; set; }
        public DbSet<ENTRADA_DOCUMENTO> EntradaDocumento { get; set; }
        public DbSet<TIPO_DOCUMENTO> TIPO_DOCUMENTO { get; set; }

        void IDataBaseContext.SaveChanges()
        {
            base.SaveChanges();
        }

        public new void Update(object entity)
        {
            base.Update(entity);
        }

        public new void Remove(object entity)
        {
            base.Remove(entity);
        }

        public new void RemoveRange(IEnumerable<object> entities)
        {
            base.RemoveRange(entities);
        }
    }
}
