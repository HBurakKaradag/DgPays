using System;
using Couchbase;
using DgPays.Domain;
using DgPays.ProductConsumer.Buckets;
using Newtonsoft.Json;

namespace DgPays.ProductConsumer.Services
{
    public class ProductService : IProductService
    {
        private IProductBucketProvider _productBucketProvider;

        public ProductService(IProductBucketProvider productBucketProvider)
        {
            _productBucketProvider = productBucketProvider;
        }
        public async Task<bool> SetProduct(string content)
        {
            IBucket productBucket = _productBucketProvider.GetBucketAsync().GetAwaiter().GetResult();
            var products = JsonConvert.DeserializeObject<List<Product>>(content);

            var collection = await productBucket.DefaultCollectionAsync();

            products.ForEach(p =>
           {
               var guid = Guid.NewGuid();
               var collectionId = $"Product_{p.Id}_{guid}";
               collection.InsertAsync(collectionId, p);
           });

            return await Task.FromResult<bool>(true);

        }
    }
}
