using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Application.Helpers;
using WakeCommerceProject.Domain;

namespace WakeCommerceProject.Application.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(QueryObject query);
        Task<Product?> GetByIdAsync(int id); //FirstOrDefault can be null
        Task<Product> CreateAsync(Product productModel);
        Task<Product?> UpdateAsync(int id, UpdateProductRequestDTO productDTO);
        Task<Product?> DeleteAsync(int id);
    }
}
