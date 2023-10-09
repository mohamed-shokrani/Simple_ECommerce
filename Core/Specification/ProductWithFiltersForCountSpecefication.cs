using Core.Entities;

namespace Core.Specification;
public class ProductWithFiltersForCountSpecefication :Specification<Product>
{
	public ProductWithFiltersForCountSpecefication(ProductSpecParams productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search )|| x.Name.ToLower().Contains(productParams.Search))&&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.ProductTypeId.HasValue || x.ProductTypeId == productParams.ProductTypeId))
    {

	}
}
