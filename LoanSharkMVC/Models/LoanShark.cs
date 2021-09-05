using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanSharkMVC.Models
{
    public class LoanShark
    {
        public decimal LoanAmount { get; set; }
        
        public int LoanTerm { get; set; }
        
        public decimal LoanRate { get; set; }

        public decimal Payment { get; set; }

        public decimal TotalInterest { get; set; }

        public decimal TotalCost { get; set; }

        public List<LoanPayment> Payments { get; set; } = new();
    }
}
