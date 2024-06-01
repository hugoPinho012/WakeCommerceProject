﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakeCommerceProject.Application.DTOs;
using WakeCommerceProject.Application.Interfaces;
using WakeCommerceProject.API.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using WakeCommerceProject.Application.Helpers;
using Moq;
using WakeCommerceProject.Domain;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WakeCommerceProject.Application.Mappings;

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

            // Criamos uma lista de produtos para os testes
            var products = new List<Product>
            {
                new Product(12, "Moletom", "Moletom vermelho", 199.99m, 1),
                new Product(13, "Chinelo", "Chinelo verde", 49.99m, 6)
            };

            // O método GetAllAsync quando chamado com queryObject retorna a lista de produtos criada anteriormente
            _mockProductRepository.Setup(repo => repo.GetAllAsync(queryObject)).ReturnsAsync(products);

            // Injetamos o repositório mock como dependência a uma instância do controlador
            var controller = new ProductController(_mockProductRepository.Object);

            //Act
            // Chamamos o método GetAllAsync que deve retornar uma Ok
            var result = await controller.GetAllAsync(queryObject) as OkObjectResult;

            //Assert
            // Verificamos se o resultado não é nulo
            Assert.NotNull(result);

            // Verificamos se o código de status HTTP retornado é 200
            Assert.Equal(200, result.StatusCode);

            var productDTOs = result.Value as IEnumerable<ProductDTO>;
            productDTOs.Should().NotBeNull();
            // Verificamos se o número de elementos na coleção é igual ao número de produtos na lista original
            productDTOs.Should().HaveCount(products.Count);
        }

        [Fact(DisplayName = "GetById Returns OK")]
        public async Task ProductController_GetByIdAsync_ReturnsOk()
        {
            // Arrange
            var id = 1;
            var product = new Product(12, "Moletom", "Moletom vermelho", 199.99m, 1); // Create a sample product
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
                Stock = 4
            };

            var productModel = productDTO.ToProductFromCreateDTO(); // Convert to product model
            productModel.Id = 1; // Set a sample product ID

            _mockProductRepository.Setup(repo => repo.CreateAsync(productModel)).ReturnsAsync(productModel);

            var controller = new ProductController(_mockProductRepository.Object);

            // Act
            var result = await controller.CreateAsync(productDTO) as CreatedAtActionResult;

            // Assert
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.ActionName.Should().Be(nameof(ProductController.GetByIdAsync));

        }

        [Fact(DisplayName = "CreateAsync With Invalid Parameters Returns Bad Request")]
        public async Task ProductController_CreateAsync_InvalidParameters_ReturnsBadRequest()
        {
            // Arrange
            var productDTO = new CreateProductRequestDTO
            {
                Name = "Corta-vento",
                Description = "Corta-vento preto",
                Price = -299.9m,
                Stock = 4
            };

            Action action = () => productDTO.ToProductFromCreateDTO();
            action.Should()
                .Throw<DomainExceptionValidation>()
                .WithMessage("Invalid price. Price cannot be negative.");

        }

        [Fact(DisplayName = "UpdateAsync Returns OK")]
        public async Task ProductController_UpdateAsync_ValidId_ReturnsOkResult()
        {
            // Arrange
            int productId = 1; // Valid product ID
            var updateDTO = new UpdateProductRequestDTO(); // Create a valid DTO

            // Mock the repository method
            _mockProductRepository.Setup(repo => repo.UpdateAsync(productId, updateDTO))
                .ReturnsAsync(new Product(1, "Chapéu", "Chapéu marrom", 45.99m, 5));

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
            var productId = 1; // ID do produto a ser excluído
            _mockProductRepository.Setup(repo => repo.DeleteAsync(productId))
                .ReturnsAsync(new Product(1, "Chapéu", "Chápeu marrom", 45.99m, 5)); // Simula que o produto foi encontrado

            var controller = new ProductController(_mockProductRepository.Object);

            // Act
            var result = await controller.DeleteAsync(productId);

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
