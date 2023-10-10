using Core.Entities;
namespace Core.Specification;//Data.Specification;

public class ProductsWithCategories : Specification<Product>
{
    public ProductsWithCategories(ProductSpecParams productParams)
        : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) &&
        (!productParams.ProductTCategoryId.HasValue || x.ProductCategoryId == productParams.ProductTCategoryId) )
    {
        AddInclude(x => x.ProductCategory);
       
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

}
