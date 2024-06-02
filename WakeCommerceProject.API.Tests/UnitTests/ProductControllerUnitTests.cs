using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WakeCommerceProject.API.Controllers;
using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Application.Helpers;
using WakeCommerceProject.Application.Interfaces;
using WakeCommerceProject.Domain;

namespace WakeCommerceProject.API.Tests.UnitTests
{
    public class ProductControllerUnitTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        public ProductControllerUnitTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();

        }

        [Fact(DisplayName = "GetProducts Returns OK")]
        public async void ProductController_GetProducts_ReturnOK()
        {
            //Arrange
            QueryObject queryObject = new QueryObject();

            // Create a sample list for the test
            var products = new List<Product>
            {
                new Product{ Id = 12, Name = "Moletom", Description = "Moletom vermelho", Price = 199.99m, Stock = 1, SKU= "AG12345"},
                new Product{ Id = 13, Name = "Chinelo", Description = "Chinelo verde", Price = 49.99m, Stock = 6, SKU= "AG12346"}
            };

            // Set up a mock behavior for the GetAllAsync method of the product repository
            _mockProductRepository.Setup(repo => repo.GetAllAsync(queryObject)).ReturnsAsync(products);

            // Inject the mock repository as a dependency into an instance of the controller
            var controller = new ProductController(_mockProductRepository.Object);

            //Act
            var result = await controller.GetAllAsync(queryObject) as OkObjectResult;

            //Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);

           
        }

        [Fact(DisplayName = "GetById Returns OK")]
        public async Task ProductController_GetByIdAsync_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var product = new Product { Id = id, Name = "Moletom", Description = "Moletom vermelho", Price = 199.99m, Stock = 1, SKU = "AE12456" }; // Create a sample product
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync(product);

            var controller = new ProductController(_mockProductRepository.Object);

            // Act
            var result = await controller.GetByIdAsync(id) as OkObjectResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
        }

        [Fact(DisplayName = "Create Product With Valid Parameters")]
        public async Task ProductController_CreateAsync_ValidParameters_ReturnsCreatedAtAction()
        {
            // Arrange
            var productDTO = new CreateProductRequestDTO
            {
                Name = "Corta-vento",
                Description = "Corta-vento preto",
                Price = 299.9m,
                Stock = 4,
                SKU = "AD1246"
            };


            var controller = new ProductController(_mockProductRepository.Object);

            // Act
            var result = await controller.CreateAsync(productDTO) as CreatedAtActionResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.ActionName.Should().Be(nameof(ProductController.GetByIdAsync));

        }



        [Fact(DisplayName = "UpdateAsync Returns OK")]
        public async Task ProductController_UpdateAsync_ValidId_ReturnsOkResult()
        {
            // Arrange
            int productId = 1; // Valid product ID
            var updateDTO = new UpdateProductRequestDTO(); // Create a valid DTO

            // Mock the repository method
            _mockProductRepository.Setup(repo => repo.UpdateAsync(productId, updateDTO))
                .ReturnsAsync(new Product
                {
                    Id = productId,
                    Name = "Chapéu",
                    Description = "Chapéu marrom",
                    Price = 45.99m,
                    Stock = 5,
                    SKU = "AD1245"
                });

            var controller = new ProductController(_mockProductRepository.Object);


            // Act
            var result = await controller.UpdateAsync(productId, updateDTO);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<OkObjectResult>();
        }

        [Fact(DisplayName = "DeleteAsync Returns No Content")]
        public async Task ProductController_DeleteAsync_ReturnsNoContent_WhenProductModelIsNotNull()
        {
            // Arrange
            var productId = 1; // Product id to be deleted
            _mockProductRepository.Setup(repo => repo.DeleteAsync(productId))
                .ReturnsAsync(new Product());

            var controller = new ProductController(_mockProductRepository.Object);

            // Act
            var result = await controller.DeleteAsync(productId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
