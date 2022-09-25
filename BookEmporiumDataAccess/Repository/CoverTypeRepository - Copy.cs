using BookEmporium.DataAccess.Data;
using BookEmporium.DataAccess.Repository.IRepository;
using BookEmporium.Models;

namespace BookEmporium.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;
        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Company obj)
        {
            _db.Company.Update(obj);
        }
    }
}
