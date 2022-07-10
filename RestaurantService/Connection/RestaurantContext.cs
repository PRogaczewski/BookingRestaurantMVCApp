using Microsoft.EntityFrameworkCore;
using RestaurantLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantLibrary.Connection
{
    public class RestaurantContext : DbContext
    {
        public RestaurantContext(DbContextOptions<RestaurantContext> options) : base(options)
        {}
        public DbSet<Restaurant> restaurants { get; set; }
        public DbSet<Address> addresses { get; set; }
        public DbSet<Tables> tables { get; set; }
        public DbSet<RestaurantTable> restaurantTable { get; set; }
        public DbSet<BookedTable> bookedTables { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Restaurant>(r =>
            {
                r.HasOne(r => r.Address)
                .WithOne(a => a.Restaurant)
                .HasForeignKey<Address>(a => a.RestaurantId);

                r.HasMany(r => r.Tables)
                .WithMany(t => t.Restaurants)
                .UsingEntity<RestaurantTable>(
                    b=>b.HasOne(bt=>bt.Table)
                    .WithMany()
                    .HasForeignKey(bt=>bt.TableId),

                    b => b.HasOne(bt => bt.Restaurant)
                    .WithMany()
                    .HasForeignKey(bt => bt.RestaurantId),

                    b=>
                    {
                        b.HasKey(x => x.Id);
                    }

                    );
            });
        }
    }
}
