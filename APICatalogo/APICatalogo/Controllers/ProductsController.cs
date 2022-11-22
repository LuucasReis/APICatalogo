using APICatalogo.DTOs;
using APICatalogo.Model;
using APICatalogo.Pagination;
using APICatalogo.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace APICatalogo.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _map;
        public ProductsController(IUnitOfWork unitOfWork, IMapper map)
        {
            _unitOfWork = unitOfWork;
            _map = map;
        }

        [HttpGet]

        public async Task<ActionResult<IEnumerable<ProductDTO>>> Get([FromQuery] ProductsParameters productsParameters)
        {
            var result =await _unitOfWork.Product.GetProduct(productsParameters);
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

            var resultDTO = _map.Map<List<ProductDTO>>(result);
            return resultDTO;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProductDTO>> Get(int id)
        {
            Product p = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            if (p == null)
            {
                return NotFound();
            }
            var productDTO = _map.Map<ProductDTO>(p);
            return productDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                var produto = _map.Map<Product>(productDTO);
                _unitOfWork.Product.Add(produto);
                await _unitOfWork.SaveAsync();

                var produtoDTO = _map.Map<ProductDTO>(produto);

                return new CreatedAtRouteResult("ObterProduto", new
                {
                    id = produto.Id
                }, produtoDTO);
            }
            return BadRequest();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, ProductDTO productDTO )
        {
            var product = _map.Map<Product>(productDTO);

            if (id != product.Id)
            {
                return BadRequest();
            }
            _unitOfWork.Product.Update(product);
            await _unitOfWork.SaveAsync();

            var pDTO = _map.Map<ProductDTO>(product);

            return Ok(pDTO);


        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProductDTO>> Delete(int id)
        {
            var product = await _unitOfWork.Product.GetFirstOrDefaultAsync(x => x.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(product);
            await _unitOfWork.SaveAsync();

            ProductDTO productDTO = _map.Map<ProductDTO>(product);

            return Ok(productDTO);
        }

    }
}
