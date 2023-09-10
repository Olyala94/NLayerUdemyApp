using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);

        //productRepository.GetAll(x=>x.id > 5).ToListAsync();
        IQueryable<T> GetAll();

        //productRepository.Where(x=>x.Id > 5 ).OrderBy.ToListAsync(); //Id 5 den olan dataları alır
        IQueryable<T> Where(Expression<Func<T, bool>> expression); //IQueryable - direk Veri tabana gitmez,çagırdığım yerde gidecek(ToListDediğimiz zaman gidecek Veri Tabanına) 

        //productRepository.Where(x=>x.Id > 5 ).OrderBy.ToListAsync(); true || false dönecek
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity); //Ama Add Memory'ye bir Entity ekliyoe , o yüzden uzun sürebilir (o yüzden Async var )

        Task AddRangeAsync(IEnumerable<T> entities); //Birden fazla kayedebilirim
        void Update(T entity); //Update - uzun süren işlem olmadığı için Async işlemi yok (kısa süren işlem , uzun süren işlem değil)

        void Remove(T entity);  //Delete - uzun süren işlem olmadığı için Async işlemi yok (kısa süren işlem , uzun süren işlem değil)

        void RemoveRange(IEnumerable<T> entities);
    }
}
