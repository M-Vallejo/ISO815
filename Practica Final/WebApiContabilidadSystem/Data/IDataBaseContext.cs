using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Models;

namespace WebApiContabilidadSystem.Data
{
    public interface IDataBaseContext
    {
        public DatabaseFacade Database { get; }

        DbSet<USUARIO> Usuario { get; set; }
        DbSet<CONCEPTO_PAGO> ConceptoPago { get; set; }
        DbSet<PROVEEDOR> Proveedor { get; set; }
        DbSet<ENTRADA_DOCUMENTO> EntradaDocumento { get; set; }
        DbSet<TIPO_DOCUMENTO> TIPO_DOCUMENTO { get; set; }

        void SaveChanges();
        void Update(object entity);
        void Remove(object entity);
        void RemoveRange(IEnumerable<object> entities);
    }
}