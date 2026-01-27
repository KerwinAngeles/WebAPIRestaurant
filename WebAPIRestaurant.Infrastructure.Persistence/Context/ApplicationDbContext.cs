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
        public DbSet<DishesOrden> DishesOrden { get; set; }
        public DbSet<Status> Status { get; set; }

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
            modelBuilder.Entity<DishesOrden>().ToTable("DishesOrden");
            modelBuilder.Entity<Status>().ToTable("Status");
            #endregion

            #region "Primary Key"
            modelBuilder.Entity<Dishe>().HasKey(d => d.Id);
            modelBuilder.Entity<Orden>().HasKey(o => o.Id);
            modelBuilder.Entity<Ingredient>().HasKey(i => i.Id);
            modelBuilder.Entity<Table>().HasKey(t => t.Id);
            modelBuilder.Entity<Status>().HasKey(s => s.Id);
            modelBuilder.Entity<DisheIngredient>().HasKey(di => new { di.DisheId, di.IngredientId });
            modelBuilder.Entity<DishesOrden>().HasKey(od => new { od.DishesId, od.OrdensId });
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

            modelBuilder.Entity<DishesOrden>()
                 .HasOne(od => od.Dishe)
                 .WithMany(o => o.DishesOrdens)
                 .HasForeignKey(od => od.DishesId);

            modelBuilder.Entity<DishesOrden>()
                .HasOne(od => od.Orden)
                .WithMany(d => d.DishesOrden)
                .HasForeignKey(od => od.OrdensId);

            modelBuilder.Entity<Orden>()
                .HasOne(o => o.Table)
                .WithOne(m => m.Orden)
                .HasForeignKey<Table>(t => t.OrdenId);

            modelBuilder.Entity<Status>()
                .HasMany(t => t.Tables)
                .WithOne(s => s.Status)
                .HasForeignKey(t => t.StatusId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Data"
            modelBuilder.Entity<Status>()
                .HasData(

                new Status { Id = 1, Name = "Disponible"},
                new Status { Id = 2, Name = "En proceso de atencion"},
                new Status { Id = 3, Name = "Atendida"}

                );
            #endregion
            base.OnModelCreating(modelBuilder);
        }
    }
}
