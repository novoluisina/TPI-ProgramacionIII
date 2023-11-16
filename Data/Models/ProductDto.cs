using System.ComponentModel.DataAnnotations;

namespace TPI_ProgramacionIII.Data.Models
{
    public class ProductDto
    {
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public int Stock { get; set; }
    }
}
