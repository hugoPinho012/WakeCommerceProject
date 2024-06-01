using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakeCommerceProject.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // TODO: Verificar isso
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public Product()
        {
           

        }

        public Product(int id, string name, string description, decimal price, int stock)
        {
            Id = id;
            Name = name;
            Description = description;
            Stock = stock;

            DomainExceptionValidation.When(decimal.IsNegative(price), "Invalid price. Price cannot be negative.");
            Price = price;

        }

        public Product(string name, string description, decimal price, int stock)
        {
            Name = name;
            Description = description;
            Stock = stock;

            DomainExceptionValidation.When(decimal.IsNegative(price), "Invalid price. Price cannot be negative.");
            Price = price;

        }
    }
}
