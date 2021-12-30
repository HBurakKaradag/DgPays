using System;
using DgPays.API.IntegratorBussiness.CheckoutAPI;

namespace DgPays.API.IntegratorBussiness
{
    public sealed class IntegratorContext
    {
        private FakeStoreBusinessContext _fakeStoreBusinessContext;
        // private CheckoutBusinessContext _checkoutBusinessContext;
    
        public FakeStoreBusinessContext FakeStoreBusinessContext => 
        _fakeStoreBusinessContext ?? (_fakeStoreBusinessContext = new FakeStoreBusinessContext(this));

        // public CheckOutBusinessContext CheckOutBusinessContext => 
        // _checkoutBusinessContext ?? (_checkoutBusinessContext = new CheckoutBusinessContext(this));

        

        private IntegratorContext()
        {
        }

        private static IntegratorContext _instance;
        private static readonly object Lock = new object();

        public static IntegratorContext Current
        {
            get
            {
                if (_instance == null)
                {
                    lock (Lock)
                    {
                        _instance = new IntegratorContext();
                    }
                }

                return _instance;
            }
        }
    }
}
