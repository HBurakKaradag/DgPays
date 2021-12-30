using System;
using DgPays.Integrators;

namespace DgPays.API.IntegratorBussiness.Abstract
{
    public abstract class IntegratorBusinessContext
    {
        
        protected abstract string BaseUrlKey { get; }
        protected IntegratorContext Context { get; }
        protected ClientFactory ClientFactory { get; }
        protected IntegratorBusinessContext(IntegratorContext context)
        {
            this.Context = context;
            this.ClientFactory = new ClientFactory();
        }
    }
}
