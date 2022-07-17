using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Product
    {
        public Product()
        {
            Invoiceitems = new HashSet<Invoiceitem>();
        }

        public string Id { get; set; }
        public string Domainserviceid { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual Domainservice Domainservice { get; set; }
        public virtual ICollection<Invoiceitem> Invoiceitems { get; set; }
    }
}
