using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNDAPEC.API.Models
{
    public class Header
    {
        public int NumeroCuenta { get; set; }
        public string RNC { get; set; }
        public decimal MontoTotal { get; set; }
        public int CantidadRegistros { get; set; }
    }
}
