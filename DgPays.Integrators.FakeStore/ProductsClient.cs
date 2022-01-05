using System;
using DgPays.Domain;
using DgPays.Integrators;
using RestSharp;

namespace DgPays.Integrators.FakeStore
{
    public class ProductsClient : JsonBaseClient
    {
        public ProductsClient(Uri baseUri) : base(baseUri)
        {
        }

        public ProductsClient(string baseUrl) : base(baseUrl)
        {
            this.OperationUrlPrefix = baseUrl;

        }

        public ApiResponse<List<Product>> Products()
        {
            const string OPERATION_URL = nameof(Products);

            var response = base.Get<List<Product>>(OPERATION_URL, null);
            return new ApiResponse<List<Product>>{ Body = response };

        }

    }
}
