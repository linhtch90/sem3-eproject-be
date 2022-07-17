﻿using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Service
    {
        public Service()
        {
            Domainservices = new HashSet<Domainservice>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Domainservice> Domainservices { get; set; }
    }
}