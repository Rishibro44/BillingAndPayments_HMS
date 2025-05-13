using Microsoft.EntityFrameworkCore;

namespace BillingAndPayments.Repository.Models
{
    public class BillingContext : DbContext
    {

        public BillingContext(DbContextOptions<BillingContext> options) : base(options) { }

        public DbSet<Bill> Bills { get; set; }
        public DbSet<UserManagement> UserManagements { get; set; }

    }
}