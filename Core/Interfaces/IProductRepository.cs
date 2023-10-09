using Core.DTO_s;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    Task<Product> GetProductByIdAsync(int id);
    Task<IReadOnlyList<ProductDto>> GetProductsAsync();

}
