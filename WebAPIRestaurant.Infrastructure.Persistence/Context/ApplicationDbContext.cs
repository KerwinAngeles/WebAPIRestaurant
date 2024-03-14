using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIRestaurant.Core.Domain.Common;
using WebAPIRestaurant.Core.Domain.Entities;

namespace WebAPIRestaurant.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Dishe> Dishes { get; set; }
        public DbSet<Orden> Ordens { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<DisheIngredient> DishesIngredients { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = "DefaultAppUser";
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.LastModifiedBy = "DefaultAppUser";
                        break;

                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region "Tables"
            modelBuilder.Entity<Dishe>().ToTable("Dishes");
            modelBuilder.Entity<Orden>().ToTable("Ordens");
            modelBuilder.Entity<Ingredient>().ToTable("Ingredients");
            modelBuilder.Entity<Table>().ToTable("Tables");
            modelBuilder.Entity<DisheIngredient>().ToTable("DishesIngredients");
            #endregion

            #region "Primary Key"
            modelBuilder.Entity<Dishe>().HasKey(d => d.Id);
            modelBuilder.Entity<Orden>().HasKey(o => o.Id);
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Table>().HasKey(t => t.Id);
            modelBuilder.Entity<DisheIngredient>().HasKey(di => new { di.DisheId, di.IngredientId });
            #endregion

            #region "Relationship"

            modelBuilder.Entity<DisheIngredient>()
                .HasOne(di => di.Ingredient)
                .WithMany(i => i.DishesIngredients)
                .HasForeignKey(di => di.IngredientId);

            modelBuilder.Entity<DisheIngredient>()
              .HasOne(di => di.Dishe)
              .WithMany(d => d.DishesIngredients)
              .HasForeignKey(di => di.DisheId);

            modelBuilder.Entity<Orden>()
                .HasMany<Dishe>(d => d.Dishes)
                .WithOne(o => o.Orden)
                .HasForeignKey(o => o.OrdenId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Table)
                .WithOne(m => m.Orden)
                .HasForeignKey<Table>(t => t.OrdenId);

            #endregion

            base.OnModelCreating(modelBuilder);
        }
    }
}
