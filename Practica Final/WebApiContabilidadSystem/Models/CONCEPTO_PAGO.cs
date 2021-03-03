using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class CONCEPTO_PAGO
    {
        [Key]
        public int CONCEPTO_PAGO_ID { get; set; }
        public string DESCRIPCION { get; set; }
        public int ESTADO { get; set; }
    }
}
