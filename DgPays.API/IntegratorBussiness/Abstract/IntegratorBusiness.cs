using System;
using DgPays.Integrators;

namespace DgPays.API.IntegratorBussiness.Abstract
{
  public abstract class IntegratorBusiness<TClient, TContext>
            where TClient : BaseClient
            where TContext : class
    {
        protected virtual TClient Client { get; }
        protected virtual TContext Context { get; }

        protected IntegratorBusiness(TClient client, TContext context)
        {
            this.Client = client;
            this.Context = context;
        }
    }
}
