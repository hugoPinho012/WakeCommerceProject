
using WakeCommerceProject.Domain;
using WakeCommerceProject.Infra.Data.Context;

namespace WakeCommerceProject.API
{
    public static class ProductsInitializer
    {
        public static WebApplication Seed(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                try
                {
                    // Ensure tat the database is created 
                    context.Database.EnsureCreated();

                    // Check if any products exits 
                    var products = context.Products.FirstOrDefault();

                    if (products == null)
                    {
                        // Add samples to the datavase
                        context.Products.AddRange(
                            new Product { Id = 1, Name = "Camiseta", Description = "Camiseta manga curta estampada", Price = 89.99m, Stock = 10, SKU = "B12345" },
                            new Product { Id = 2, Name = "Casaco", Description = "Casaco moletom liso", Price = 299.99m, Stock = 8, SKU = "AB12346" },
                            new Product { Id = 3, Name = "Calça", Description = "Calça jeans", Price = 399.99m, Stock = 5, SKU = "AB12347" },
                            new Product { Id = 4, Name = "Boné", Description = "Boné vermelho e cinza", Price = 59.99m, Stock = 12, SKU = "AB12348" },
                            new Product { Id = 5, Name = "Tênis", Description = "Tênis de corrida", Price = 559.99m, Stock = 3, SKU = "AB12349" }
                        );

                        context.SaveChanges();
                    }
                }
                catch (Exception)
                {
                    // Handle any exceptions that occur during database operations
                    throw;
                }
                return app;
            }
        }
    }
}