using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiContabilidadSystem.Models;

namespace WebApiContabilidadSystem.AccountingService
{
    public class ServicioContabilidad
    {
        public string description { get; set; }
        public int idAuxiliarSystem { get; set; }
        public string movementType { get; set; }
        public int account { get; set; }
        public decimal seatAmount { get; set; }
        public ServicioContabilidad Contabilizar(List<ENTRADA_DOCUMENTO> documentos, int idAuxiliar, int cuenta)
        {
            const string movType = "CR";
            this.description = $"Asiento contable de cuentas por pagar del periodo {documentos[0].FECHA_DOCUMENTO.Year}-{documentos[0].FECHA_DOCUMENTO.ToString("MM")}";
            this.idAuxiliarSystem = idAuxiliar;
            this.movementType = movType;
            this.account = cuenta;
            this.seatAmount = documentos.Sum(x => x.MONTO);
            return this;
        }
    }

    
}
