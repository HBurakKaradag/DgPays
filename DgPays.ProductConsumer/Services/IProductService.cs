using System;

namespace DgPays.ProductConsumer.Services
{
    public interface IProductService
    {
        Task<bool> SetProduct(string content);
    }
}
