using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                new Product(1, "Camiseta", "Camiseta manga curta estampada", 89.99m, 10),
                new Product(2, "Casaco", "Casaco moletom liso", 299.99m, 8),
                new Product(3, "Calça", "Calça jeans", 399.99m, 5),
                new Product(4, "Boné", "Boné vermelho e cinza", 59.99m, 12),
                new Product(5, "Tênis", "Tênis de corrida", 559.99m, 3)
            };
        }
    }
}
