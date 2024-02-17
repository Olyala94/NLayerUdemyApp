using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System.Linq.Expressions;

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productCache";
        private readonly IMapper _mapper;
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductServiceWithCaching(IMapper mapper, IMemoryCache memoryCache, IProductRepository repository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _memoryCache = memoryCache;
            _repository = repository;
            _unitOfWork = unitOfWork;

            //boş karakter memoride yer tutmasın diye bıraktım - sadece Bu CacheProductKey sahip Data var mı yok mu bunu öğrenmek istedim(yani Cach'lediği datayı almak istemiyor, oyüzden Memoride boşuna Avoked etmesin diye "_" alttere ile memoride avokeyd etmesini engelliyorum) 
            if (!_memoryCache.TryGetValue(CacheProductKey, out _))
            {
                //eger yok ise _memoryCache.Set et  tüm datayı el ve listele
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);

                //Program.cs de ayarlama yapılacak (Cach'i Aktif etmememiz lazım)
                // =>  builder.Services.AddMemoryCache(); eklenecek
            }

        }

        public async Task<Product> AddAsync(Product entity)
        {
            //Cach'liyecegimiz Data Çok Sık Erişecegin Data ama Çok Sık Güncellemediğin bir şey olması lazım!!!

            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entity;
        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
            return entities;
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            var prodocts = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            return Task.FromResult(prodocts);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                throw new NotFoundExeption($"{typeof(Product).Name} Id:{id} : not found.");
            }

            return Task.FromResult(product);
        }

        public Task<CustomResponseDto<List<ProductWithCategory>>> GetProductsWithCategory()
        {
            var products = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);

            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategory>>(products);

            return Task.FromResult(CustomResponseDto<List<ProductWithCategory>>.Success(200, productsWithCategoryDto));
        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();
        }

        public async Task CacheAllProductsAsync()
        {
            //Bu Method ne Yapıyo  ? - Her çağırdığımda sıfırdan Data'yı çekip Cachliyor
            _memoryCache.Set(CacheProductKey, await _repository.GetAll().ToListAsync());
        }
    }
}
