using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;

namespace Data
{
    public class CarDealerContext : IdentityDbContext<IdentityUser>
    {
        public CarDealerContext()
        {
        }

        public DbSet<CustomerEntity> Customers { get; set; }
        public DbSet<SellerEntity> Sellers { get; set; }
        public DbSet<CarEntity> Cars { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var path = System.IO.Path.Join(Directory.GetParent(Environment.CurrentDirectory).FullName, "Data", "cardealer.db");

            System.Diagnostics.Debug.WriteLine($"Database path: {path}");

            options.UseSqlite($"Data Source={path};Mode=ReadWrite");
         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CustomerEntity>().HasData(
                new CustomerEntity() { Id = 1, Name = "Miłosz Filimowski", Email = "milosz.fil@gmail.com" }
            );

            modelBuilder.Entity<SellerEntity>().HasData(
                new SellerEntity() { Id = 1, Name = "Jan Kowalski", Email = "jan.kowalski@gmail.com" }
            );

            modelBuilder.Entity<CarEntity>().HasData(
                new CarEntity() { Id = 1, Brand = "Toyota", Model = "Corolla", Price = 10000 }
            );

            modelBuilder.Entity<TransactionEntity>().HasData(
                new TransactionEntity() { Id = 1, CustomerId = 1, SellerId = 1, CarId = 1, Price = 10000 }
            );

            string ADMIN_ID = Guid.NewGuid().ToString();
            string ADMIN_ROLE_ID = Guid.NewGuid().ToString();

            // dodanie roli administratora
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "admin",
                NormalizedName = "ADMIN",
                Id = ADMIN_ROLE_ID,
                ConcurrencyStamp = ADMIN_ROLE_ID
            });

            // utworzenie administratora jako użytkownika
            var admin = new IdentityUser
            {
                Id = ADMIN_ID,
                Email = "adminuser@wsei.edu.pl",
                EmailConfirmed = true,
                UserName = "adminuser@wsei.edu.pl",
                NormalizedUserName = "ADMINUSER@WSEI.EDU.PL",
                NormalizedEmail = "ADMINUSER@WSEI.EDU.PL"
            };

            // haszowanie hasła, najlepiej wykonać to poza programem i zapisać gotowy
            // PasswordHash
            PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = ph.HashPassword(admin, "S3cretPassword");

            // zapisanie użytkownika
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            // przypisanie roli administratora użytkownikowi
            modelBuilder.Entity<IdentityUserRole<string>>()
            .HasData(new IdentityUserRole<string>
            {
                RoleId = ADMIN_ROLE_ID,
                UserId = ADMIN_ID
            });

        }
    }
}
