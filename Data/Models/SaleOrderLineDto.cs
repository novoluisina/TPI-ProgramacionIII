using System.ComponentModel.DataAnnotations.Schema;
using TPI_ProgramacionIII.Data.Entities;

namespace TPI_ProgramacionIII.Data.Models
{
    public class SaleOrderLineDto
    {
        public int Amount { get; set; }
        public decimal UnitPrice { get; set; }
        public int ProductId { get; set; }
        public int SaleOrderId { get; set; }
    }
}
