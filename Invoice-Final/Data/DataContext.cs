using Invoice_Final.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Final.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<tblCustomer> tblCustomer { get; set; }
        public DbSet<tblCar> tblCar { get; set; }
        public DbSet<tblInvoice> tblInvoice { get; set; }
        public DbSet<tblInvoiceItem> tblInvoiceItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one-to-many connection tblCustomer-tblCar
            modelBuilder.Entity<tblCustomer>()
                .HasMany(c => c.tblCar)
                .WithOne(ca => ca.tblCustomer)
                .HasForeignKey(ca => ca.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // one-to-many tblCar-tblInvoice
            modelBuilder.Entity<tblCar>()
                .HasMany(ca => ca.tblInvoice)
                .WithOne(i => i.tblCar)
                .HasForeignKey(i => i.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // one-to-many tblInvoice-tblInvoiceItem
            modelBuilder.Entity<tblInvoice>()
                .HasMany(i => i.tblInvoiceItem)
                .WithOne(ii => ii.tblInvoice)
                .HasForeignKey(ii => ii.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
