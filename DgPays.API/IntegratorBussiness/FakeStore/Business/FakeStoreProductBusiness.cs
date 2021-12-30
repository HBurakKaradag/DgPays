using System;
using DgPays.API.IntegratorBussiness.Abstract;
using DgPays.API.IntegratorBussiness.CheckoutAPI;
using DgPays.Domain;
using DgPays.Integrators.FakeStore;

namespace DgPays.API.IntegratorBussiness.FakeStore.Business
{
    public class FakeStoreProductBusiness : IntegratorBusiness<ProductsClient, FakeStoreBusinessContext>
    {

        public ApiResponse<List<Product>> GetAllProducts()
        {
            var products =  base.Client.Products();

            return products;
        }

        public FakeStoreProductBusiness(ProductsClient client, FakeStoreBusinessContext context)
          : base(client, context)
        {

        }
    }
}
