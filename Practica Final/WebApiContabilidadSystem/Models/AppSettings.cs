using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ValidMinutes { get; set; }
    }
}
