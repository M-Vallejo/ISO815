using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    [Table("PROVEEDOR")]
    public class PROVEEDOR
    {
        [Key]
        public int PROVEEDOR_ID { get; set; }
        public string NOMBRE { get; set; }
        public int TIPO_PERSONA { get; set; }
        public int TIPO_DOCUMENTO { get; set; }
        public string NUMERO_DOCUMENTO { get; set; }
        public decimal BALANCE { get; set; }
        public int ESTADO { get; set; }
    }
}
