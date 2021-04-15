using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.ViewModels
{
    public class EntradaDocumentViewModel
    {
        public int NUMERO_FACTURA { get; set; }
        public int NUMERO_CHEQUE { get; set; }
        public DateTime FECHA_DOCUMENTO { get; set; }
        public decimal MONTO { get; set; }
        public int TIPO_DOCUMENTO_ID { get; set; }
        public int PROVEEDOR_ID { get; set; }
        public string CONDICIONES { get; set; }

        public ENTRADA_DOCUMENTO ToEntity() 
        {
            return new ENTRADA_DOCUMENTO
            {
                NUMERO_FACTURA = this.NUMERO_FACTURA,
                NUMERO_CHEQUE = this.NUMERO_CHEQUE,
                FECHA_DOCUMENTO = this.FECHA_DOCUMENTO,
                MONTO = this.MONTO,
                TIPO_DOCUMENTO = new TIPO_DOCUMENTO 
                {
                    TIPO_DOCUMENTO_ID = this.TIPO_DOCUMENTO_ID
                },
                PROVEEDOR = new PROVEEDOR
                {
                    PROVEEDOR_ID = this.PROVEEDOR_ID
                },
                CONDICIONES = this.CONDICIONES,
                ESTADO = 0,
                FECHA_REGISTRO = DateTime.Now
            };
        }
    }
}
