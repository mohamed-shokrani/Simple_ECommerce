using Core.DTO_s;
using Core.Entities;
using Core.Helper;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams);
}
