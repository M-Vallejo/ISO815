using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FUNDAPEC.API.Models
{
    public class Payment
    {
        public Header Cabecera { get; set; }
        public IEnumerable<Detail> Detalles { get; set; }

        public Payment(IEnumerable<Detail> detalles)
        {
            this.Cabecera = new Header 
            {
                NumeroCuenta = 123456789,
                CantidadRegistros = detalles.Count(),
                MontoTotal = detalles.Sum(x=> x.Monto),
                RNC = "401014017",
            };

            this.Detalles = detalles;
        }
    }
}
