using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.ViewModels
{
    public class AccountingSearchViewModel
    {
        public DateTime Desde { get; set; }
        public DateTime Hasta { get; set; }
    }
}
