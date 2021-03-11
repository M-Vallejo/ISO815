using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class USUARIO
    {
        [Key]
        [JsonPropertyName("usuario_id")]
        public int USUARIO_ID { get; set; }
        [JsonPropertyName("nombre_usuario")]
        public string NOMBRE_USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string CORREO { get; set; }
        public int ROL { get; set; }
        [JsonPropertyName("fecha_creacion")]
        public DateTime FECHA_CREACION { get; set; }
        public int ESTADO { get; set; }

    }
}
