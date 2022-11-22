using APICatalogo.Data;
using APICatalogo.Model;
using APICatalogo.Pagination;
using APICatalogo.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace APICatalogo.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly CatalogoDbContext _context;
        public CategoryRepository(CatalogoDbContext context) : base(context)    
        {
            _context = context;
        }

        public async Task<PagedList<Category>> GetCategorieHeader(CategoryParameters paginationParameters)
        {
            return PagedList<Category>.ToPagedList(await GetAllAsync(), paginationParameters.PageNumber, paginationParameters.PageSize);
        }

        public void Update(Category obj)
        {
            _context.Categories.Update(obj);
        }

    }
}
