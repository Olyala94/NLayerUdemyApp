using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    //Burada IGeneric'den gelen methodlarım var artı eklediğim bu => GetProductsWithCategory() metodumda var
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
