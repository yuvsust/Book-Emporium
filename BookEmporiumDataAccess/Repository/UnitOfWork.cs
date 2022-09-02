
using BookEmporium.DataAccess.Data;
using BookEmporium.DataAccess.Repository.IRepository;

namespace BookEmporium.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(db);
        }
        public ICategoryRepository Category { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
