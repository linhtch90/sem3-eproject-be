using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Contactinfo
    {
        public string Id { get; set; }
        public string Address { get; set; }
        public string Ward { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}
