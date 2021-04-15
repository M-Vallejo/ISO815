using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.ViewModels
{
    public class TransactionsViewModel
    {
        public IEnumerable<int> Transactions { get; set; }
    }
}
