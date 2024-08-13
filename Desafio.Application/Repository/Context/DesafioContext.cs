using Desafio.Application.domain;
using Microsoft.EntityFrameworkCore;

namespace Desafio.Application.Repository.Context;

public class DesafioContext : DbContext
{
    public DesafioContext(DbContextOptions<DesafioContext> options) : base(options)
    {
        this.Database.EnsureCreated();
    }

    public virtual DbSet<User> User { get; set; }
    public virtual DbSet<Product> Product { get; set; }
    public virtual DbSet<Sale> Sale { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Cpf);
            entity.Property(e => e.Name);
            entity.Property(e => e.Email);
            entity.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
            ;
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Name);
            entity.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt).ValueGeneratedOnAdd();
            entity.HasOne(e => e.User)
                .WithMany()
                .HasForeignKey(s => s.UserId);
        });


        modelBuilder.Entity<SaleProduct>(entity =>
        {
            entity.HasKey(sp => new { sp.SaleId, sp.ProductId });

            entity.HasOne(sp => sp.Sale)
                .WithMany(s => s.SaleProducts)
                .HasForeignKey(sp => sp.SaleId);

            entity.HasOne(sp => sp.Product)
                .WithMany()
                .HasForeignKey(sp => sp.ProductId);
        });

        seed(modelBuilder);
    }

    private void seed(ModelBuilder modelBuilder)
    {
        var user1 = new User
            { Id = 1, Name = "robin", Cpf = "05741329705", Email = "robin@gmail.com", Phone = "21999999999" };
        var user2 = new User
            { Id = 2, Name = "Wolverine", Cpf = "85741329705", Email = "Wolverine@gmail.com", Phone = "21999999999" };
        var user3 = new User
            { Id = 3, Name = "Batima", Cpf = "95741329705", Email = "batima@gmail.com", Phone = "21999999999" };

        modelBuilder.Entity<User>().HasData(
            user1,
            user2,
            user3
        );

        var product1 = new Product { Id = 1, Name = "Luvas de boxe" };
        var product2 = new Product { Id = 2, Name = "Garras de adamantium" };
        var product3 = new Product { Id = 3, Name = "bat-carro" };
        var product4 = new Product { Id = 4, Name = "bat-moto" };
        var product5 = new Product { Id = 5, Name = "bat-aviao" };
        var product6 = new Product { Id = 6, Name = "bat-suco" };
        var product7 = new Product { Id = 7, Name = "Bat-range" };


        modelBuilder.Entity<Product>().HasData(
            product1,
            product2,
            product3,
            product4,
            product5,
            product6,
            product7
        );

        modelBuilder.Entity<Sale>().HasData(
            new Sale
            {
                Id = 1,
                UserId = user1.Id,
                Status = EnumStatusSale.AGUARDANDO_PAGAMENTO
            });

        modelBuilder.Entity<SaleProduct>().HasData(
            new SaleProduct
            {
                SaleId = 1,
                ProductId = 1
            });

        /*modelBuilder.Entity<Sale>().HasData(
            new Sale
            {
                Id = 1,
                UserId = user1.Id,
                User = user1,
                Products = [product1],
                Status = EnumStatusSale.AGUARDANDO_PAGAMENTO
            },
            new Sale
            {
                Id = 2,
                UserId = user2.Id,
                User = user2,
                Products = [product5],
                Status = EnumStatusSale.AGUARDANDO_PAGAMENTO
            },
            new Sale
            {
                Id = 3,
                UserId = user3.Id,
                User = user3,
                Products = [product1],
                Status = EnumStatusSale.AGUARDANDO_PAGAMENTO
            }
        );*/
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseInMemoryDatabase("InMemoryDb")
                .EnableSensitiveDataLogging();
        }
    }
}