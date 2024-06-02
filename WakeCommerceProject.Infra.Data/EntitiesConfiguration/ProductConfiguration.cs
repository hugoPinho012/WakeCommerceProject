using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WakeCommerceProject.Domain;

namespace WakeCommerceProject.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Stock).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(200);
            builder.Property(p => p.Price).HasPrecision(10, 2);
            builder.Property(p => p.SKU).HasMaxLength(200);


            builder.HasData(
                new Product { Id = 1, Name = "Camiseta", Description = "Camiseta manga curta estampada", Price = 89.99m, Stock = 10, SKU = "B12345" },
                new Product { Id = 2, Name = "Casaco", Description = "Casaco moletom liso", Price = 299.99m, Stock = 8, SKU = "AB12346" },
                new Product { Id = 3, Name = "Calça", Description = "Calça jeans", Price = 399.99m, Stock = 5, SKU = "AB12347" },
                new Product { Id = 4, Name = "Boné", Description = "Boné vermelho e cinza", Price = 59.99m, Stock = 12, SKU = "AB12348" },
                new Product { Id = 5, Name = "Tênis", Description = "Tênis de corrida", Price = 559.99m, Stock = 3, SKU = "AB12349" }
                );
        }
    }
}
