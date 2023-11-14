namespace TPI_ProgramacionIII.Data.Entities
{
    public class Client : User
    {
        public string Address { get; set; }
        public ICollection<SaleOrder> SalesOrders { get; set; } = new List<SaleOrder>();
    }
}
