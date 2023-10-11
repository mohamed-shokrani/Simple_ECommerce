using Core.Entities;
using Core.Helper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
//[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductsController( IProductRepository productRepository)
    {

        _productRepository = productRepository;
    }
    
    [HttpGet]
     public async Task<IReadOnlyList<Product>> GetAllProductsWithCategories([FromQuery] ProductParams? productParams )
         => await _productRepository.GetAllProductsAsync(productParams);



    [HttpGet("Categories")]
    public async Task<IReadOnlyList<ProductCategory>> GetAllCategories()
         => await _productRepository.GetAllCategories();



    [HttpGet("GetSinglle{id}")]
    public async Task<Product> GetAllCategories(int id)
        => await _productRepository.GetSingleById(id);

}
