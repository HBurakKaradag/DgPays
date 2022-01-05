using DgPays.API.IntegratorBussiness;
using DgPays.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DgPays.API.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpGet]
    public ActionResult Get()
    {
        var products = _productService.GetAllProduct();
        return Ok(products);
      
    }
}