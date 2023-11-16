
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;

namespace TPI_ProgramacionIII.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        int CreateProduct(Product product);
        void DeleteProduct(int id);
        Product UpdateProduct(Product product);
    }
}
