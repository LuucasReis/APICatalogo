using APICatalogo.Model;
using APICatalogo.Pagination;

namespace APICatalogo.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);
        Task<PagedList<Category>> GetCategorieHeader(CategoryParameters paginationParameters);
    }
}
