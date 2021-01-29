using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PUSH.Models
{
    public class Employee
    {
        [Key]
        public int AccountNumber { get; set; }
        public char DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public decimal Amount { get; set; }
    }
}
