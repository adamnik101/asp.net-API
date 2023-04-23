using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Termin3.DataAccess.Configurations;
using Termin3.DataAccess.Entities;
namespace Termin3.DataAccess
{
    public class Termin3Context : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var groups = new List<Group>()
            {
                new Group { Id = 1, Name = "Grupa 1"},
                new Group { Id = 2, Name = "Grupa 2"},
                new Group { Id = 3, Name = "Grupa 3"},
            };
            modelBuilder.Entity<Group>().HasData(groups);

            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());

            modelBuilder.Entity<Group>().HasQueryFilter(x => !x.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(x => !x.IsDeleted);


            modelBuilder.Entity<OrderProduct>().HasKey(x => new { x.OrderId, x.ProductId});
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source= .\SQLEXPRESS; Initial Catalog= Termin3; Integrated Security=true");
        }
        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if(entry.Entity is Entity e)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added: 
                            e.IsActive = true;
                            e.CreatedAt = DateTime.Now;
                            e.IsDeleted = false;
                            e.DeletedAt = null;
                            e.ModifiedAt = null;
                            break;
                        case EntityState.Modified:
                            e.ModifiedAt = DateTime.Now;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products{ get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
