using NLayer.Core.UnitOfWorks;

namespace NLayer.Repository.UnitOfWork
{
    public class UnitOFWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnitOFWork(AppDbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
