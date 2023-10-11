using Api.Helper;
using Core.DTO_s;
using Core.Entities;
using Core.Helper;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProductsController : ControllerBase
{
    private readonly IProductRepository _productRepository;
    public ProductsController( IProductRepository productRepository)
    {

        _productRepository = productRepository;
    }
    
    [HttpGet]
     public async Task<IReadOnlyList<Product>> GetAll([FromQuery] ProductParams? productParams )
    {
        return  await _productRepository.GetAllProductsAsync(productParams);
    }
}
