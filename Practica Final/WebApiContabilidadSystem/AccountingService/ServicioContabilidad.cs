using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.AccountingService
{
    public class ServicioContabilidad
    {
        public string description { get; set; }
        public int idAuxiliarSystem { get; set; }
        public string movementType { get; set; }
        public int account { get; set; }
        public decimal seatAmount { get; set; }
        public string entryDate { get; set; }
        public string Procesar(int Total, int IdAuxiliar, string cuentadebito, string cuentaCredito, string descripcion)
        {
            return "";
        }
    }

    
}
