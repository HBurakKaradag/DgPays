using System;

namespace DgPays.Domain
{
    public class ApiResponse<T> 
    {
        public string Status { get; set; }
        public T Body { get; set; }

    }
}
