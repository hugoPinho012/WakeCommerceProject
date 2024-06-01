using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
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

            builder.HasData(
                new Product(1, "Camiseta", "Camiseta manga curta estampada", 89.99m, 10),
                new Product(2, "Casaco", "Casaco moletom liso", 299.99m, 8),
                new Product(3, "Calça", "Calça jeans", 399.99m, 5),
                new Product(4, "Boné", "Boné vermelho e cinza", 59.99m, 12),
                new Product(5, "Tênis", "Tênis de corrida", 559.99m, 3)
                );
        }
    }
}
