using RestSharp;

namespace DgPays.Integrators
{
    public abstract class JsonBaseClient : BaseClient
    {

        protected JsonBaseClient(Uri baseUrl)
            : base(baseUrl)
        {
            this.AddHandler("application/json", () => NewtonsoftJsonSerializer.Default);
            this.AddHandler("text/javascript", () => NewtonsoftJsonSerializer.Default);
            this.AddDefaultHeader("Content-Type", "application/json; charset=utf-8;");
        }

        protected JsonBaseClient(string url)
            : this(new Uri(url))
        {

        }
    }
}
