using System;
using System.Collections.Generic;
using System.Text;

namespace PUSH.Models
{
    public class Header
    {
        public char Type { get; } = 'E';
        public char DocumentType { get; set; }
        public string DocumentNumber { get; set; }
        public string AccountNumber { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime Period { get; set; }

    }
}
