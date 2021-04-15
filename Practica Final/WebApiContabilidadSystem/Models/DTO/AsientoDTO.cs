using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.DTO
{
    public class AsientoDTO
    {
        [JsonPropertyName("id_transaccion")]
        public int Id_Transaccion { get; set; }
        public string Descripcion { get; set; }
        [JsonPropertyName("fecha_transaccion")]
        public DateTime Fecha_Transaccion { get; set; }
        public decimal Monto { get; set; }


        public AsientoDTO( ENTRADA_DOCUMENTO doc)
        {
            this.Id_Transaccion = doc.NUMERO_DOCUMENTO;
            this.Descripcion = "Entrada de documento";
            this.Fecha_Transaccion = doc.FECHA_DOCUMENTO;
            this.Monto = doc.MONTO;
        }
    }
}
