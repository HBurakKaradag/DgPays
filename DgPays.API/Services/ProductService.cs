using DgPays.API.IntegratorBussiness;
using DgPays.Domain;

namespace DgPays.API.Services
{
    public class ProductService : IProductService
    {
        public async Task<ApiResponse<List<Product>>> GetAllProduct()
        {
            var productResponse = IntegratorContext.Current.FakeStoreBusinessContext.ProductBusiness.GetAllProducts();
            return await Task.FromResult(productResponse);
        }

    }
}
