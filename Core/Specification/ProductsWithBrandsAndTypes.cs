using Core.Entities;
namespace Core.Specification;//Data.Specification;

public class ProductsWithBrandsAndTypes : Specification<Product>
{
    public ProductsWithBrandsAndTypes(ProductSpecParams productParams)
        : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.BrandId.HasValue || x.ProductBrandId == productParams.BrandId) &&
        (!productParams.ProductTypeId.HasValue || x.ProductTypeId == productParams.ProductTypeId))
    {
        AddInclude(x => x.productType);
        AddInclude(x => x.productBrand);
        AddOrderBy(x => x.Name);//Add Order by Alpha by Default
        ApplyPaging(productParams.pageSize * (productParams.indexPage - 1), productParams.pageSize);

        if (!string.IsNullOrEmpty(productParams.Sort))
        {
            switch (productParams.Sort)
            {

                case "priceAsc":
                    AddOrderBy(x => x.Price); break;
                case "priceDesc":
                    AddOrderByDescending(x => x.Price); break;
                default: AddOrderBy(x => x.Name); break;
            }

        }

    }
    public ProductsWithBrandsAndTypes(int id)
        : base(x => x.Id == id)
    {
        AddInclude(x => x.productType);
        AddInclude(x => x.productBrand);
    }
}
