using APICatalogo.Data;
using APICatalogo.Model;
using APICatalogo.Pagination;
using APICatalogo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly CatalogoDbContext _db;
        public ProductRepository(CatalogoDbContext db) : base(db)
        {
                _db = db;
        }

        public async Task<PagedList<Product>> GetProduct(ProductsParameters paginationParameters)
        {
            return  PagedList<Product>.ToPagedList(await GetAllAsync(), paginationParameters.PageNumber
                , paginationParameters.PageSize);
        }

        public void Update(Product product)
        {
            _db.Products.Update(product);
        }
    }
}
