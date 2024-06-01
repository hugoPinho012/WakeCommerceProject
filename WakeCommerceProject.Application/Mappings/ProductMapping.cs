using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Domain;

namespace WakeCommerceProject.Application.Mappings
{
    public static class ProductMapping
    {
        public static ProductDTO ToProductDTO(this Product productModel)
        {

            return new ProductDTO
            {
                Id = productModel.Id,
                Name = productModel.Name,
                Description = productModel.Description,
                Price = productModel.Price,
                Stock = productModel.Stock,
            };
        }

        public static Product ToProductFromCreateDTO(this CreateProductRequestDTO productDTO)
        {
            return new Product(
                productDTO.Name, 
                productDTO.Description ?? "", 
                productDTO.Price, 
                productDTO.Stock);
        }
    }
}
