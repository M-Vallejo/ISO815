using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FUNDAPEC.API.Models
{
    public class Detail
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }
        public char TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public string Matricula { get; set; }
        public decimal Monto { get; set; }
        public char TipoPago { get; set; }
    }
}
