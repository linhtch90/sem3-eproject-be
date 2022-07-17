using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Domainservice
    {
        public Domainservice()
        {
            Products = new HashSet<Product>();
        }

        public string Id { get; set; }
        public string Domainid { get; set; }
        public string Serviceid { get; set; }

        public virtual Domain Domain { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
