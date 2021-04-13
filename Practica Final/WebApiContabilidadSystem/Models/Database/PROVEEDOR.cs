using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    [Table("PROVEEDOR")]
    public class PROVEEDOR
    {
        [Key]
        [JsonPropertyName("proveedor_id")]
        public int PROVEEDOR_ID { get; set; }

        [JsonPropertyName("nombre")]
        public string NOMBRE { get; set; }

        [JsonPropertyName("tipo_persona")]
        public int TIPO_PERSONA { get; set; }

        [JsonPropertyName("tipo_documento")]
        public int TIPO_DOCUMENTO { get; set; }

        [JsonPropertyName("numero_documento")]
        public string NUMERO_DOCUMENTO { get; set; }

        [JsonPropertyName("balance")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal BALANCE { get; set; }

        [JsonPropertyName("estado")]
        public int ESTADO { get; set; }

    }
}
