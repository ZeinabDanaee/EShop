using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using IDP.Domain.Entities;

namespace IDP.Infra.Data
{
    public class ShopCommandDbContext:DbContext
    {
        protected readonly IConfiguration Configration;
        public ShopCommandDbContext(IConfiguration configuration)
        {
            Configration=configuration; 
        }


      
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer(Configration.GetConnectionString("CommandDbConnectionString")); 

        }

        public DbSet<User> users { get; set; }  
    }
}
