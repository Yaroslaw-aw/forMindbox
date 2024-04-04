using Microsoft.AspNetCore.Mvc;
using ProductsCategoriesManyWithMany.Db;
using ProductsCategoriesManyWithMany.Dto;

namespace ProductsCategoriesManyWithMany.Repositories
{
    public interface ITestProductCategory
    {
       public Task<List<GetProductCategoryDto>?> GetProductsWithCategoriesAsync();
       public Task<Guid?> AddProductAsync(string productName);
       public Task<Guid?> AddCategoryAsync(string categoryName);
       public Task<ProductCategory?> AddProductCategoryAsync(PostProductCategoryDto productCategoryDto);
       public Task<IEnumerable<GetProductDto>?> GetAllProductsAsync();
       public Task<IEnumerable<GetCategoryDto>?> GetAllCategoriesAsync();
    }
}
