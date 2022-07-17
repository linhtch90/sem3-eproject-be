using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Invoice
    {
        public Invoice()
        {
            Invoiceitems = new HashSet<Invoiceitem>();
        }

        public string Id { get; set; }
        public DateTime Createat { get; set; }
        public decimal Totalprice { get; set; }
        public string Status { get; set; }
        public string Userid { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Invoiceitem> Invoiceitems { get; set; }
    }
}
