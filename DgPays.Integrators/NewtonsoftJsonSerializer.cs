using System;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace DgPays.Integrators
{
    public class NewtonsoftJsonSerializer : ISerializer, IDeserializer
    {

        private JsonSerializerSettings JsonSerializerSettings {get;}

        private Formatting Formatting {get;}

        public NewtonsoftJsonSerializer(Formatting formatting, JsonSerializerSettings jsonSerializerSettings)
        {
            this.Formatting = formatting;
            this.JsonSerializerSettings = jsonSerializerSettings ?? new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
        }

        public string ContentType { get => "application/json"; set {} }

        public T? Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content, this.JsonSerializerSettings);
        }

        public string? Serialize(object obj)
        {

            return JsonConvert.SerializeObject(obj,this.Formatting,this.JsonSerializerSettings);

        }

        public static NewtonsoftJsonSerializer Default => new NewtonsoftJsonSerializer(Formatting.None, new JsonSerializerSettings{
            NullValueHandling = NullValueHandling.Ignore
        });

    }
}
