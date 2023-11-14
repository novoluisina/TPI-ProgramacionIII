
using TPI_ProgramacionIII.Data.Entities;
using TPI_ProgramacionIII.Data.Models;

namespace TPI_ProgramacionIII.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductDto> GetProducts();
        ProductDto GetProductById(int id);
    }
}
