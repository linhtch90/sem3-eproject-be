using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Invoiceitem
    {
        public string Id { get; set; }
        public string Invoiceid { get; set; }
        public string Productid { get; set; }
        public int Quantity { get; set; }
        public decimal Totalprice { get; set; }

        public virtual Invoice Invoice { get; set; }
        public virtual Product Product { get; set; }
    }
}
