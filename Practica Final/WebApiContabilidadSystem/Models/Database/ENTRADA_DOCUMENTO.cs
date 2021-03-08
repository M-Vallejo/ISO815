using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class ENTRADA_DOCUMENTO
    {
        [Key]
        public int NUMERO_DOCUMENTO { get; set; }
        public int NUMERO_FACTURA { get; set; }
        public DateTime FECHA_DOCUMENTO { get; set; }
        public decimal MONTO { get; set; }
        public DateTime FECHA_REGISTRO { get; set; }
        public int PROVEEDOR { get; set; }
        public int ESTADO { get; set; }

    }
}
