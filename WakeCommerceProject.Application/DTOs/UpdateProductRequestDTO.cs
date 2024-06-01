using System.ComponentModel.DataAnnotations;

namespace WakeCommerceProject.Application.DTOs
{
    public class UpdateProductRequestDTO
    {
        [Required]
        [MinLength(2, ErrorMessage = "Name must be 2 characters")]
        [MaxLength(100, ErrorMessage = "Name cannot be over 100 characters")]
        public string Name { get; set; } = string.Empty;
        // TODO: Verificar isso
        [MaxLength(500, ErrorMessage = "Description cannot be over 500 characters")]
        public string? Description { get; set; } = string.Empty;

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Price cannot be negative")]
        public decimal Price { get; set; }

        [Required]
        public int Stock { get; set; }
    }
}
