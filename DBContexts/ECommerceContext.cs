using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using TPI_ProgramacionIII.Controllers;
using TPI_ProgramacionIII.Data.Entities;

namespace TPI_ProgramacionIII.DBContexts
{
    public class ECommerceContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<SaleOrder> SaleOrders { get; set; }
        public DbSet<SaleOrderLine> SaleOrderLines { get; set; }
        public DbSet<Product> Products { get; set; }
       

        //Acá estamos llamando al constructor de DbContext que es el que acepta las opciones
        public ECommerceContext(DbContextOptions<ECommerceContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator(u => u.UserType);

            modelBuilder.Entity<Client>().HasData(
                new Client
                {
                    LastName = "Gomez",
                    Name = "Nicolas",
                    Email = "ngomez@gmail.com",
                    UserName = "ngomez_cliente",
                    Password = "123456",
                    Address = "Rivadavia 111",
                    Id = 1
                },
                new Client
                {
                    LastName = "Perez",
                    Name = "Juan",
                    Email = "Jperez@gmail.com",
                    UserName = "jperez",
                    Password = "123456",
                    Address = "J.b.justo 111",
                    Id = 2
                },
                new Client
                {
                    LastName = "Garcia",
                    Name = "Jose",
                    Email = "jgarcia@gmail.com",
                    UserName = "jgarcia",
                    Password = "123456",
                    Address = "San Martin 111",
                    Id = 3
                });

            modelBuilder.Entity<Admin>().HasData(
                new Admin
                {
                    LastName = "Novo",
                    Name = "Luisina",
                    Email = "lnovo@gmail.com",
                    UserName = "lnovo",
                    Password = "123456",
                    Id = 4,
                    Role = "admin"
                },
                new Admin
                {
                    LastName = "Bruno",
                    Name = "Diaz",
                    Email = "bdiaz@gmail.com",
                    UserName = "bdiaz",
                    Password = "123456",
                    Id = 5,
                    Role = "admin"
                });

            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 6,
                    Name = "Remera Fiberton",
                    Price = 1250,
                    Stock = 10,  

                },
                new Product
                {
                    Id = 7,
                    Name = "Remera Loren",
                    Price = 1320,
                    Stock = 15,
                });

            

            // // Relación entre Cliente y OrdenDeVenta (uno a muchos)
            modelBuilder.Entity<Client>()
           .HasMany(c => c.SaleOrders)
           .WithOne(o => o.Client)
           .HasForeignKey(o => o.ClientId);

            // Relación entre OrdenDeVenta y LineaDeVenta (uno a muchos)
            modelBuilder.Entity<SaleOrder>()
                .HasMany(o => o.SaleOrderLines)
                .WithOne(l => l.SaleOrder)
                .HasForeignKey(l => l.SaleOrderId);


            modelBuilder.Entity<SaleOrderLine>()
                .HasOne(sol => sol.Product)
                .WithMany() //vacío porque no me interesa establecer esa relación
                .HasForeignKey(sol => sol.ProductId);



            base.OnModelCreating(modelBuilder);

        }
    }

}

