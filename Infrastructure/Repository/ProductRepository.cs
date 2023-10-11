using Core.DTO_s;
using Core.Entities;
using Core.Interfaces;
using Core.Helper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Repository;
public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public ProductRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PageList<Product>> GetAllProductsAsync(ProductParams productParams)
    {
        var query = _dbContext.Products.Include(x => x.ProductCategory).AsQueryable();
        if (productParams.CategoryId is not null)
        {
            query = query.Where(x => x.ProductCategoryId == productParams.CategoryId);
        }
        query = productParams.OrderBy switch
            {
                "priceAsc" => query.OrderBy(x => x.Price),
                "priceDsc" => query.OrderByDescending(x => x.Price),
                 _ => query.OrderBy(x => x.Name),
            };
        return await PageList<Product>.CreateAsync(query
                                          ,productParams.PageNumber, productParams.PageSize);
            
    }
}
