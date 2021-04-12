﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models
{
    public class ENTRADA_DOCUMENTO
    {
        [Key]
        [JsonPropertyName("numero_documento")]
        public int NUMERO_DOCUMENTO { get; set; }
        [JsonPropertyName("numero_factura")]
        public int NUMERO_FACTURA { get; set; }
        [JsonPropertyName("numero_cheque")]
        public int NUMERO_CHEQUE { get; set; }
        [JsonPropertyName("fecha_documento")]
        public DateTime FECHA_DOCUMENTO { get; set; }
        public decimal MONTO { get; set; }
        [JsonPropertyName("tipo_documento")]
        public int TIPO_DOCUMENTO { get; set; }
        public virtual TIPO_DOCUMENTO TipoDocumento { get; set; }
        public int PROVEEDOR { get; set; }
        public virtual PROVEEDOR proveedor { get; set; }
        [JsonPropertyName("id_asiento")]
        public int ID_ASIENTO { get; set; }
        public string CONDICIONES { get; set; }
        [JsonPropertyName("fecha_registro")]
        public DateTime FECHA_REGISTRO { get; set; }
        public int ESTADO { get; set; }

    }
}
