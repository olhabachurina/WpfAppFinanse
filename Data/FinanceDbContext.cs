using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfAppFinanse.Models;

namespace WpfAppFinanse.Data
{
    public class FinanceDbContext : DbContext
    {
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Wallet> Wallets { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite("Data Source=finance.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Wallet>()
                .HasOne(w => w.Currency)
                .WithMany()
               .HasForeignKey(w => w.CurrencyId);

            modelBuilder.Entity<Transaction>()
                .HasOne<Wallet>()
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.AccountId);
        }
    }
}
