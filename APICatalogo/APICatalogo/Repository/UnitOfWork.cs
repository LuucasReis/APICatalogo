using APICatalogo.Data;
using APICatalogo.Repository.IRepository;

namespace APICatalogo.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly CatalogoDbContext _db;

        public IProductRepository Product { get; private set; }
        public ICategoryRepository Category { get; private set; }

        public UnitOfWork(CatalogoDbContext db)
        {
            _db = db;
            Product = new ProductRepository(_db);
            Category = new CategoryRepository(_db);
        }
        public async Task SaveAsync()
        {
           await  _db.SaveChangesAsync();
        }
    }
}
