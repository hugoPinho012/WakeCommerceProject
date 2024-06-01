using FluentAssertions;

namespace WakeCommerceProject.Domain.Tests
{
    public class ProductUnitTest
    {
        [Fact(DisplayName ="Create Product With Valid Params")]
        public void CreateProduct_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99);
            action.Should().NotThrow<WakeCommerceProject.Domain.DomainExceptionValidation>();
        }


        [Fact(DisplayName = "Create Product With Invalid Params")]
        public void CreateProduct_WithNegativePrice_DomainExceptionInvalidId()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", -9.99m, 99);
            action.Should()
                .Throw<WakeCommerceProject.Domain.DomainExceptionValidation>()
                .WithMessage("Invalid price. Price cannot be negative.");
        }
    }
}