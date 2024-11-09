using FFoodTerminal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FFoodTerminal.Domain.Enum;
using FFoodTerminal.Domain.Helpers;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FFoodTerminal.DataAccessLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<ProductEntity> ProductsDbSet { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<ProductEntity>().ToTable("Products");
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=PP;Database=FoodDb;Trusted_Connection=True;TrustServerCertificate=True");

        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<UserEntity>().ToTable("Users");

        //}

        public DbSet<UserEntity> UsersDbSet { get; set; }

        public DbSet<ProfileEntity> ProfilesDbSet { get; set; }

        public DbSet<Basket> BasketsDbSet { get; set; }

        public DbSet<Order> OrdersDbSet { get; set; }

        //  с помощью modelBuilder соотносим и настраиваем объекты и шарпа с объектами в БД
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(builder =>
            {
                builder.HasData(new UserEntity
                {
                    Id = 1,
                    Name = "PPPPPP",
                    Password = HashPasswordHelper.HashPassowrd("123456"),
                    Role = Role.Admin
                });

                builder.ToTable("UsersDbSet").HasKey(x => x.Id);
                builder.Property(x => x.Id)
                    .ValueGeneratedOnAdd();

                builder.Property(x => x.Password).IsRequired();
                builder.Property(x => x.Name).HasMaxLength(100).IsRequired();

                builder.HasOne(x => x.ProfileEntity)
                    .WithOne(x => x.UserEntity)
                    .HasPrincipalKey<UserEntity>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);

                builder.HasOne(x => x.Basket)
                    .WithOne(x => x.UserEntity)
                    .HasPrincipalKey<UserEntity>(x => x.Id)
                    .OnDelete(DeleteBehavior.Cascade);
            });


            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProfileEntity>().ToTable("Profiles");

            modelBuilder.Entity<ProfileEntity>(builder =>
            {
                builder.ToTable("Profiles").HasKey(x => x.Id);

                builder.Property(x => x.Id).ValueGeneratedOnAdd();
                builder.Property(x => x.Age);
                builder.Property(x => x.Address).HasMaxLength(200).IsRequired(false);

            });

            modelBuilder.Entity<Basket>(builder =>
            {
                builder.ToTable("Baskets").HasKey(x => x.Id);

                //builder.HasData(new Basket()
                //{
                //    Id = 1,
                //    UserEntityId = 1
                //});
            });

            modelBuilder.Entity<Order>(builder =>
            {
                builder.ToTable("Orders").HasKey(x => x.Id);

                builder.HasOne(r => r.Basket).WithMany(t => t.Orders)
                    .HasForeignKey(r => r.BasketId);
            });

        }

   
    }
}
