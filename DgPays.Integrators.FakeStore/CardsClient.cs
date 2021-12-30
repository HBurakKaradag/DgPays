using System;
using DgPays.Domain;
using DgPays.Integrators;
using RestSharp;

namespace DgPays.Integrators.FakeStore
{
    public class CardsClient : JsonBaseClient
    {
        public CardsClient(Uri baseUri) : base(baseUri)
        {
        }

        public CardsClient(string baseUrl) : base(baseUrl)
        {
            this.OperationUrlPrefix = baseUrl + "/cards/";
        }

        public ApiResponse<List<object>> Cards()
        {

            const string OPERATION_URL = nameof(Cards);

            var response = base.Get<ApiResponse<List<object>>>(OPERATION_URL, null);
            return response;


        }
    }
}
