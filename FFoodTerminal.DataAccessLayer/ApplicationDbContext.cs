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
            });
        }

    }
}
