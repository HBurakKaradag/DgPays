using DgPays.API.IntegratorBussiness;
using DgPays.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DgPays.API.Controllers;

[ApiController]
[Route("[controller]/[Action]")]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    [HttpPost]
    public async Task<ActionResult> GetProductById([FromBody] ProductReqeustModel req)
    {
        if (!req.ProductId.HasValue)
            return BadRequest();

        var products = await _productService.GetAllProduct();
        var filteredProduct = products.Body.Where(p => p.Id == req.ProductId);

        return Ok(filteredProduct);
    }


    [HttpGet]
    public ActionResult Get()
    {
        var products = _productService.GetAllProduct();
        return Ok(products);

    }
}