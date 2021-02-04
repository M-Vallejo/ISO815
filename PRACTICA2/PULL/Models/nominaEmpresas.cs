using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PULL.Models
{

    public class nominaEmpresas
    {
        [Key]
        public int NominaId { get; set; }
        public string TipoDocumentoEmpresa { get; set; }
        public string DocumentoEmpresa { get; set; }
        public string CuentaEmpresa { get; set; }
        public string NumCuenta { get; set; }
        public string TipoDocumento { get; set; }
        public string Documento { get; set; }
        public decimal Monto { get; set; }
        public string Fecha { get; set; }

    }
}
