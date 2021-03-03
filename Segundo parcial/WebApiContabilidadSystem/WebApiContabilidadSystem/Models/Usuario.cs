using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class USUARIO
    {
        [Key]
        public int USUARIO_ID { get; set; }
        public string NOMBRE_USUARIO { get; set; }
        public string CLAVE { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDOS { get; set; }
        public string CORREO { get; set; }
        public int ROL { get; set; }
        public DateTime FECHA_CREACION { get; set; }
        public int ESTADO { get; set; }

    }
}
