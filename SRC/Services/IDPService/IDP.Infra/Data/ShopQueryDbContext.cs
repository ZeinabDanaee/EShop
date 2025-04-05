using IDP.Domain.Entites;
using IDP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IDP.Infra.Data
{
    public class ShopQueryDbContext: DbContext
    {
        protected readonly IConfiguration Configuration;
        public ShopQueryDbContext(IConfiguration configuration)
        {
            Configuration = configuration;

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            options.UseSqlServer(Configuration.GetConnectionString("QueryDbConnectionString"));
        }
        public DbSet<User> users { get; set; }
        public DbSet<Outbox> Tbl_Outbox { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
