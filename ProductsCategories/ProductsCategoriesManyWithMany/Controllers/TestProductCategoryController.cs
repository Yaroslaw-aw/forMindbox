using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesManyWithMany.Caching;
using ProductsCategoriesManyWithMany.Db;
using ProductsCategoriesManyWithMany.Dto;
using ProductsCategoriesManyWithMany.Repositories;

namespace ProductsCategoriesManyWithMany.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestProductCategoryController : Controller
    {
        private readonly ITestProductCategory repository;
        private readonly Redis redis;

        public TestProductCategoryController(ITestProductCategory repository, Redis redis)
        {
            this.repository = repository;
            this.redis = redis;
        }

        [HttpGet(template: "GetAllProductsAndCategories")]
        public async Task<ActionResult<IEnumerable<GetProductCategoryDto>?>> GetAllProductsAndCategories()
        {
            var productsAndCategoriesCache = await redis.GetDataAsync<IEnumerable<GetProductCategoryDto>>("productsAndCategories");

            if (productsAndCategoriesCache is not null) 
                return Accepted(nameof(GetAllProductsAndCategories), productsAndCategoriesCache);

            var productsAndCategories = await repository.GetProductsWithCategoriesAsync();

            if (productsAndCategories is null) 
                throw new ArgumentNullException("Products are not exists");

            await redis.SetDataAsync("productsAndCategories", productsAndCategories);

            return Accepted(nameof(GetAllProductsAndCategories), productsAndCategories);
        }

        [HttpGet(template: "GetAllProducts")]
        public async Task<ActionResult<IEnumerable<GetProductDto>>> GetAllProducts()
        {
            IEnumerable<GetProductDto>? productsDtoCache = await redis.GetDataAsync<IEnumerable<GetProductDto>>("products");

            if ( productsDtoCache is not null)
                return Accepted(nameof(GetAllProducts), productsDtoCache);

            var products = await repository.GetAllProductsAsync();

            if (products is null)
                throw new ArgumentNullException("Products are not exists");

            await redis.SetDataAsync("products", products);

            return Accepted(nameof(GetAllProducts), products);
        }

        [HttpGet(template: "GetAllCategories")]
        public async Task<ActionResult<IEnumerable<GetCategoryDto>>> GetAllCategories()
        {
            var categorysDtoCache = await redis.GetDataAsync<IEnumerable<GetCategoryDto>>("categories");

            if (categorysDtoCache is not null)
                return Accepted(nameof(GetAllCategories), categorysDtoCache);

            var categories = await repository.GetAllCategoriesAsync();

            if (categories is null)
                throw new ArgumentNullException("Categories are not exists");

            await redis.SetDataAsync("categories", categories);

            return Accepted(nameof(GetAllCategories), categories);
        }

        [HttpPost(template: "AddProduct")]
        public async Task<ActionResult<Guid?>> AddProduct([FromBody] string productName)
        {
            Guid? newProductId = await repository.AddProductAsync(productName);

            if (newProductId is null)
                throw new ArgumentNullException("Product id can't be null");
            else if (newProductId.ToString() == "00000000-0000-0000-0000-000000000000")
                return Conflict("Duplicate product");

            await redis.cache.RemoveAsync("products");
            await redis.cache.RemoveAsync("productsAndCategories");

            return CreatedAtAction(nameof(AddProduct), newProductId);
        }

        [HttpPost(template: "AddCategory")]
        public async Task<ActionResult<Guid?>> AddCategory([FromBody] string categoryName)
        {
            Guid? newCategoryId = await repository.AddCategoryAsync(categoryName);

            if (newCategoryId is null)
                throw new ArgumentNullException("Product id can't be null");
            else if (newCategoryId.ToString() == "00000000-0000-0000-0000-000000000000")
                return Conflict("Duplicate category");

            await redis.cache.RemoveAsync("categories");
            await redis.cache.RemoveAsync("productsAndCategories");

            return Created(nameof(AddCategory), newCategoryId);
        }

        [HttpPost(template: "AddProductCategory")]
        public async Task<ActionResult<ProductCategory?>> AddProductCategory([FromBody] PostProductCategoryDto postProductCategoryDto)
        {
            ProductCategory? newProductCategory = await repository.AddProductCategoryAsync(postProductCategoryDto);

            if (newProductCategory is null)
                throw new ArgumentNullException("Product id can't be null");
            else if (newProductCategory.ProductId is null && newProductCategory.CategoryId is null)
                return Conflict("Product or Category doesn't exist");

            await redis.cache.RemoveAsync("productsAndCategories");

            return Created(nameof(AddProductCategory), newProductCategory);
        }
    }
}
