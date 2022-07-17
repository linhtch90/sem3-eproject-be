using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Project
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
    }
}
