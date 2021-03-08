using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace WebApiContabilidadSystem.Models
{
    [Table("CONCEPTO_PAGO")]

    public class CONCEPTO_PAGO
    {
        [Key]
        [JsonPropertyName("Concepto_pago_id")]
        public int CONCEPTO_PAGO_ID { get; set; }
        public string DESCRIPCION { get; set; }
        public int ESTADO { get; set; }
    }
}
