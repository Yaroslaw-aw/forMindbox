using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductsCategoriesManyWithMany.Db;
using ProductsCategoriesManyWithMany.Dto;

namespace ProductsCategoriesManyWithMany.Repositories
{
    public class TestProductCategory : ITestProductCategory
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;

        public TestProductCategory(AppDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<GetProductCategoryDto>?> GetProductsWithCategoriesAsync()
        {
            var productsCategories = new List<GetProductCategoryDto>().AsQueryable();

            var result = new List<GetProductCategoryDto>();

            using (context)
            {
                productsCategories = from p in context.Products
                                     join pc in context.ProductCategories on p.ProductId equals pc.ProductId into pcGroup
                                     from pc in pcGroup.DefaultIfEmpty()
                                     join c in context.Categories on pc.CategoryId equals c.CategoryId into cGroup
                                     from c in cGroup.DefaultIfEmpty()
                                     select new GetProductCategoryDto
                                     {
                                         ProductName = p.ProductName,
                                         CategoryName = c != null ? c.CategoryName : null
                                     };

                result = await productsCategories.AsNoTracking().ToListAsync();
            }
            return result;
        }


        public async Task<Guid?> AddCategoryAsync(string categoryName)
        {
            var newCategory = new Category() { CategoryName = categoryName };

            using (context)
            {
                if (await context.Categories.AnyAsync(c => c.CategoryName == categoryName))
                    return new Guid("00000000-0000-0000-0000-000000000000");

                await context.Categories.AddAsync(newCategory);
                await context.SaveChangesAsync();
            }
            return newCategory.CategoryId;
        }

        public async Task<Guid?> AddProductAsync(string productName)
        {
            var newProduct = new Product() { ProductName = productName };

            using (context)
            {
                if (await context.Products.AnyAsync(p => p.ProductName == productName))
                    return new Guid("00000000-0000-0000-0000-000000000000");

                await context.Products.AddAsync(newProduct);
                await context.SaveChangesAsync();
            }
            return newProduct.ProductId;
        }

        public async Task<ProductCategory?> AddProductCategoryAsync(PostProductCategoryDto productCategoryDto)
        {
            if (productCategoryDto.ProductId is null) return null;

            bool isCategoryExists = await isCategoryExistAsync(productCategoryDto.CategoryId);
            bool isProductExists = await isProductExistAsync(productCategoryDto.ProductId);

            if (isCategoryExists is false || isProductExists is false)
            {
                return new ProductCategory { CategoryId = null, ProductId = null };
            }
            else
            {
                ProductCategory newProductCategory = mapper.Map<ProductCategory>(productCategoryDto);

                using (context)
                {
                    await context.ProductCategories.AddAsync(newProductCategory);
                    await context.SaveChangesAsync();
                }
                return newProductCategory;
            }
        }

        public async Task<IEnumerable<GetProductDto>?> GetAllProductsAsync()
        {
            IEnumerable<Product>? products = new List<Product>();

            using (context) products = await context.Products.AsNoTracking().ToListAsync();            

            if (products is null) return null;

            IEnumerable<GetProductDto>? getProducts = new List<GetProductDto>();

            getProducts = mapper.Map(products, getProducts);

            return getProducts;
        }

        public async Task<IEnumerable<GetCategoryDto>?> GetAllCategoriesAsync()
        {
            IEnumerable<Category>? categories = new List<Category>();

            using (context) categories = await context.Categories.AsNoTracking().ToListAsync();            

            if (categories is null) return null;

            IEnumerable<GetCategoryDto>? getCategories = new List<GetCategoryDto>();

            getCategories = mapper.Map(categories, getCategories);

            return getCategories;
        }

        private async Task<bool> isProductExistAsync(Guid? productId)
        {
            return await context.Products.AnyAsync(p => p.ProductId == productId);
        }

        private async Task<bool> isCategoryExistAsync(Guid? categoryId)
        {
            return await context.Categories.AnyAsync(c => c.CategoryId == categoryId);
        }
    }
}
