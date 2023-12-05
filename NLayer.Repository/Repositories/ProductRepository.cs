using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            //Include() metodu ile ==>"Eager Loading" yaptım(Data'yı çekerken Categorylerinde alınmasını istedim)
            //Eger Product'a bağlı Category'yide ihtiyaç olduğunda daha sonra çekersek o da ==>"Lazy Loidng" olur.
            return await _context.Products.Include(x => x.Category).ToListAsync(); //Bu => "Eager Loading" (ilk Products'ları çektiğimiz anda Category'leride çekersek bu => "Eager Loading" olur.

        }
    }
}
