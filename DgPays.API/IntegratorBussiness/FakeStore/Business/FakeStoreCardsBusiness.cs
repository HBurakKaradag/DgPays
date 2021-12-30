using DgPays.API.IntegratorBussiness.Abstract;
using DgPays.API.IntegratorBussiness.CheckoutAPI;
using DgPays.Domain;
using DgPays.Integrators.FakeStore;

namespace DgPays.API.IntegratorBussiness.FakeStore.Business
{
   public class FakeStoreCardsBusiness : IntegratorBusiness<CardsClient, FakeStoreBusinessContext>
    {
        public ApiResponse<List<object>> GetAllCards()
        {
           return base.Client.Cards();

           
        }

        public FakeStoreCardsBusiness(CardsClient client, FakeStoreBusinessContext context)
          : base(client, context)
        { }
    }
}
