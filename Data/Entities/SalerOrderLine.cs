using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace TPI_ProgramacionIII.Data.Entities
{
    public class SalerOrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        [ForeignKey("ProductId")]
        public int ProductId { get; set; } // Clave foránea para la relación con Producto
        public Product Product { get; set; }
        [ForeignKey("SaleOrderId")]
        public int SaleOrderId { get; set; } // Clave foránea para la relación con OrdenDeVenta
        public SaleOrder SaleOrder { get; set; }
    }
}
