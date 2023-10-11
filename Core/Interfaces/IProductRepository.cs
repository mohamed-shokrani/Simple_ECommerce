using Core.DTO_s;
using Core.Entities;
using Core.Helper;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<IReadOnlyList<ProductCategory>> GetAllCategories();
    Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams);
    Task<Product> GetSingleById(int id);
}
