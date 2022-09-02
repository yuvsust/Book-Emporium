using BookEmporium.DataAccess.Data;
using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;

namespace BookEmporium.DataAccess.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
