using DgPays.API.IntegratorBussiness;
using DgPays.Domain;

namespace DgPays.API.Services
{
    public class ProductService : IProductService
    {
        public async Task<ApiResponse<List<Product>>> GetAllProduct()
        {
            var productResponse = IntegratorContext.Current.FakeStoreBusinessContext.ProductBusiness.GetAllProducts();

            var publishedProducts = productResponse.Body.Where(p => p.Rating.Rate > 3).ToList();

            await Publishers.ProductPublisher.Publish(publishedProducts);
           
            return await Task.FromResult(productResponse);
        }

    }
}
