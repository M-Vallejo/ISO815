using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class TIPO_DOCUMENTO
    {
        [Key]
        [JsonPropertyName("tipo_documento_id")]
        public int TIPO_DOCUMENTO_ID { get; set; }
        public string DESCRIPCION { get; set; }
        public int ESTADO { get; set; }
        public virtual ICollection<ENTRADA_DOCUMENTO> ENTRADA_DOCUMENTO { get; set; }

    }
}
