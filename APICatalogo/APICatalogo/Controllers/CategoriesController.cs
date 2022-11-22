using APICatalogo.DTOs;
using APICatalogo.Model;
using APICatalogo.Pagination;
using APICatalogo.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CategoriesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;

        public CategoriesController(IUnitOfWork unitOfWork, IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get([FromQuery] CategoryParameters paginationParameters)
        {
            var result = await _unitOfWork.Category.GetCategorieHeader(paginationParameters);
            if (result == null)
            {
                return NotFound();
            }

            var metadata = new
            {
                result.TotalCount,
                result.PageSize,
                result.CurrentPage,
                result.TotalPages,
                result.HasNext,
                result.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            var categoryDTO = _map.Map<List<CategoryDTO>>(result);

            return categoryDTO;
        }

        [HttpGet("produtos")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategoryProducts()
        {
            var result = await _unitOfWork.Category.GetAllAsync(x => x.Id <= 2, includeProperties: "Products");
            if (result == null)
            {
                return NotFound();
            }

            var catDTO = _map.Map<List<CategoryDTO>>(result);

            return catDTO;
        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            Category c =  await _unitOfWork.Category.GetFirstOrDefaultAsync(x => x.Id == id);
            if (c == null)
            {
                return NotFound();
            }

            var catDTO = _map.Map<CategoryDTO>(c);

            return catDTO;
        }

        [HttpPost]
        public async  Task<ActionResult> Post(CategoryDTO category)
        {
            if (ModelState.IsValid)
            {

                var cat = _map.Map<Category>(category);

                _unitOfWork.Category.Add(cat);
                await _unitOfWork.SaveAsync();

                var catDTO = _map.Map<CategoryDTO>(cat);

                return new CreatedAtRouteResult("ObterCategoria", new { id = catDTO.Id }, catDTO);
            }
            return BadRequest();

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {

                var cat = _map.Map<Category>(category);

                _unitOfWork.Category.Update(cat);
                await _unitOfWork.SaveAsync();

                var catDTO = _map.Map<CategoryDTO>(cat);

                return Ok(catDTO);
            }
            return BadRequest();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CategoryDTO>> Delete(int id)
        {
            if(id == 0)
            {
                return NotFound();
            }
            var cat = await _unitOfWork.Category.GetByIdAsync(id);
            _unitOfWork.Category.Remove(cat);
            await _unitOfWork.SaveAsync();
            var catDTO = _map.Map<CategoryDTO>(cat);

            return Ok(catDTO);
        }
    }
}
