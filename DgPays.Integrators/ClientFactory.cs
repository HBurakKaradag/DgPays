using System;

namespace DgPays.Integrators
{
    public class ClientFactory
    {
        public virtual T Create<T>(string configurationKey) where T : BaseClient
        {
            string url = string.Empty;
            switch (configurationKey)
            {
                case "CheckoutAPI":
                    url = "blabla"; // GetConfiguration URL BYKEY 
                    break;
                case "FakeStoreAPIBaseUrl":
                    url = "https://fakestoreapi.com/"; // GetConfiguration URL BYKEY 
                    break;
                default:
                    break;
            }

        return (T)Activator.CreateInstance(typeof(T), url);

        }

    }
}
