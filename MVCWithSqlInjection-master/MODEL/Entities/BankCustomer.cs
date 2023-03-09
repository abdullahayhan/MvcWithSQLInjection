using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MODEL.Entities
{
    public class BankCustomer
    {
        [Key]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public decimal CustomerBalance  { get; set; }
    }
}
