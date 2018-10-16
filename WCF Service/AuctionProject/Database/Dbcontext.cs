namespace Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using Core;
    using Database.Migrations;


    public class Dbcontext : DbContext
    {
        public Dbcontext()
            : base("name=DbContext")
        {
            Database.SetInitializer(new
                MigrateDatabaseToLatestVersion<Dbcontext, Configuration>());
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Auction> Auctions { get; set; }
        public virtual DbSet<Bid> Bids { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Auction>()
            .HasRequired(p => p.Product)
            .WithMany()
            .WillCascadeOnDelete(true);
        }
    }
}
