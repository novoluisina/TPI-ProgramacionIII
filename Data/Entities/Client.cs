namespace TPI_ProgramacionIII.Data.Entities
{
    public class Client : User
    {
        public string Address { get; set; }
        public ICollection<SaleOrder> SaleOrders { get; set; } = new List<SaleOrder>();
    }
}
