using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiContabilidadSystem.Models.ViewModels
{
    public class ContabilidadViewModel
    {
        [Key]
        public int id { get; set; }
        public string description { get; set; }
        public int idAuxiliarSystem { get; set; }
        public string account { get; set; }
        public string movementType { get; set; }
        public DateTime? entryDate { get; set; }
        public decimal seatAmount { get; set; }
        public bool status { get; set; }
    }
}
