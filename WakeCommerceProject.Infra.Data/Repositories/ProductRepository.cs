using Microsoft.EntityFrameworkCore;
using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Application.Helpers;
using WakeCommerceProject.Application.Interfaces;
using WakeCommerceProject.Domain;
using WakeCommerceProject.Infra.Data.Context;

namespace WakeCommerceProject.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> CreateAsync(Product productModel)
        {
            await _context.Products.AddAsync(productModel);
            await _context.SaveChangesAsync();
            return productModel;
        }

        public async Task<Product?> DeleteAsync(int id)
        {
            var productModel = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (productModel == null)
            {
                return null;
            }

            _context.Products.Remove(productModel);
            await _context.SaveChangesAsync();

            return productModel;
        }

        public async Task<List<Product>> GetAllAsync(QueryObject query)
        {
            // Creates an IQueryable for querying the 'Products' entity
            var products = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(query.Name))
            {
                products = products.Where(p => p.Name.Contains(query.Name));
            }

            if (!string.IsNullOrEmpty(query.SKU))
            {
                products = products.Where(p => p.SKU.Contains(query.SKU));
            }

            if (!string.IsNullOrEmpty(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.Name) : products.OrderBy(p => p.Name);
                }

                if (query.SortBy.Equals("SKU", StringComparison.OrdinalIgnoreCase))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.SKU) : products.OrderBy(p => p.SKU);
                }

                if (query.SortBy.Equals("Price"))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.Price) : products.OrderBy(p => p.Price);
                }

                if (query.SortBy.Equals("Stock"))
                {
                    products = query.IsDescending ? products.OrderByDescending(p => p.Stock) : products.OrderBy(p => p.Stock);
                }
            }

            return await products.ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Product?> UpdateAsync(int id, UpdateProductRequestDTO productDTO)
        {
            var existingProduct = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = productDTO.Name;
            existingProduct.Description = productDTO.Description;
            existingProduct.Price = productDTO.Price;
            existingProduct.Stock = productDTO.Stock;

            await _context.SaveChangesAsync();

            return existingProduct;
        }
    }
}

