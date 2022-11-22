using APICatalogo.Model;
using APICatalogo.Pagination;
using System.Linq.Expressions;

namespace APICatalogo.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>
    {
        void Update(Product product);
        Task<PagedList<Product>> GetProduct(ProductsParameters productsParameters);
    }
}
