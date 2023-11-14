using TPI_ProgramacionIII.Data.Entities;

namespace TPI_ProgramacionIII.Repository.Interfaces
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
    }
}
