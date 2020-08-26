using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using PMUnifiedAPI.Models;

namespace PMDataSynchronizer
{
    public class PseudoMarketsDbContext : DbContext
    {

        public string connectionString = "";

        public PseudoMarketsDbContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use the SQL Server Entity Framework Core connector
            optionsBuilder.UseSqlServer(connectionString);
        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Tokens> Tokens { get; set; }
        public DbSet<ApiKeys> ApiKeys { get; set; }
        public DbSet<Accounts> Accounts { get; set; }
        public DbSet<Positions> Positions { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Transactions> Transactions { get; set; }
    }
}
