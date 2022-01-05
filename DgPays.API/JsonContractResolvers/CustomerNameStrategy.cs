using Newtonsoft.Json.Serialization;

namespace DgPays.API.JsonContractResolvers
{
    public class CustomerNameStrategy : NamingStrategy
    {
        protected override string ResolvePropertyName(string name)
        {
          

            return $"_rsv_{name}";
        }

    }
}
