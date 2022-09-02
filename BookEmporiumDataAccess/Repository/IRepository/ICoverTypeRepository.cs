using BookEmporium.Models;

namespace BookEmporium.DataAccess.Repository.IRepository
{
    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        public void Update(CoverType obj);
    }
}
