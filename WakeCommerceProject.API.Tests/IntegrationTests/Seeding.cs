using WakeCommerceProject.Domain;
using WakeCommerceProject.Infra.Data.Context;

namespace WakeCommerceProject.API.Tests.IntegrationTests
{
    public class Seeding
    {
        public static void InitializeTestDb(ApplicationDbContext db)
        {
            db.Products.AddRange(GetProducts());
            db.SaveChanges();
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>()
            {
                new Product{ Id = 1, Name = "Camiseta", Description = "Camiseta manga curta estampada", Price = 89.99m, Stock = 10, SKU="AC12345" },
                new Product{ Id = 2, Name = "Casaco", Description = "Casaco moletom liso", Price = 299.99m, Stock = 8 , SKU="AC12346"},
                new Product{ Id = 3, Name = "Calça", Description = "Calça jeans", Price = 399.99m, Stock = 5 , SKU="AC12347"},
                new Product{ Id = 4, Name = "Boné", Description = "Boné vermelho e cinza", Price = 59.99m, Stock = 12, SKU="AC12348" },
                new Product{ Id = 5, Name = "Tênis", Description = "Tênis de corrida", Price = 559.99m, Stock = 3 , SKU="AC12349"}
            };
        }
    }
}
