using System;
using System.Collections.Generic;

#nullable disable

namespace allu_decor_be.Models
{
    public partial class Feedback
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string ProductId { get; set; }
        public string Content { get; set; }
        public DateTime Createat { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }
    }
}
