using allu_decor_be.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace allu_decor_be.Helpers
{
    public class DataContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Aboutu> Aboutus { get; set; }
        public virtual DbSet<Contactinfo> Contactinfos { get; set; }
        public virtual DbSet<Customerreview> Customerreviews { get; set; }
        public virtual DbSet<Domain> Domains { get; set; }
        public virtual DbSet<Domainservice> Domainservices { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoiceitem> Invoiceitems { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Service> Services { get; set; }

        private readonly IConfiguration Configuration;

        public DataContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // in memory database used for simplicity, change to a real db for production applications
            options.UseNpgsql(Configuration.GetConnectionString("PostgresqlConnection"));
        }
    }
}
