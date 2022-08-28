﻿using System;
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
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Domainid { get; set; }
        public string Serviceid { get; set; }

        public virtual Domain Domain { get; set; }
        public virtual Service Service { get; set; }
        public virtual ICollection<Invoiceitem> Invoiceitems { get; set; }
        public virtual ICollection<Feedback> Feedbacks { get; set; }
    }
}
