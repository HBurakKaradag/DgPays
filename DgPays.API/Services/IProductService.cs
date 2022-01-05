using System;
using DgPays.Domain;

namespace DgPays.API.Services
{
    public interface IProductService
    {
        Task<ApiResponse<List<Product>>> GetAllProduct();

    }
}
