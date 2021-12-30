using DgPays.API.IntegratorBussiness.Abstract;
using DgPays.API.IntegratorBussiness.FakeStore.Business;
using DgPays.Integrators.FakeStore;

namespace DgPays.API.IntegratorBussiness.CheckoutAPI
{
    public class FakeStoreBusinessContext : IntegratorBusinessContext
    {
        protected override string BaseUrlKey { get; }

        private FakeStoreProductBusiness _products;
        private FakeStoreCardsBusiness _cards;

        public FakeStoreProductBusiness ProductBusiness => _products
                ?? (_products = new FakeStoreProductBusiness(base.ClientFactory.Create<ProductsClient>(this.BaseUrlKey), this));

        public FakeStoreCardsBusiness CardsBusiness => _cards
           ?? (_cards = new FakeStoreCardsBusiness(base.ClientFactory.Create<CardsClient>(this.BaseUrlKey), this));

        public FakeStoreBusinessContext(IntegratorContext context)
            : base(context)
        {
            this.BaseUrlKey = "FakeStoreAPIBaseUrl";
        }
    }
}
