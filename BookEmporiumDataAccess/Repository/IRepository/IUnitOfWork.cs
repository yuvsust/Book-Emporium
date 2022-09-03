
namespace BookEmporium.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        public ICategoryRepository Category { get; }
        public ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        public void Save();
    }
}
