using Newtonsoft.Json.Serialization;

namespace DgPays.API.JsonContractResolvers
{
    public class CustomerNameStrategy : NamingStrategy
    {
        
        protected override string ResolvePropertyName(string name)
        {
            // _rsv_Id
            // _rsv_CustomerName
            // _rsv_Age

            return $"_rsv_{name}";
        }

    }
}
