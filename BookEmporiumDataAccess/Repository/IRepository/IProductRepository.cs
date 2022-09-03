
using BookEmporium.Models;

namespace BookEmporium.DataAccess.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        public void Update(Product obj);
    }
}
