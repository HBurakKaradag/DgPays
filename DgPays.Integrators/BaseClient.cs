using System.Net;
using RestSharp;

namespace DgPays.Integrators;


public abstract class BaseClient : RestClient
{

    protected virtual string OperationUrlPrefix { get; set; }

    protected BaseClient(Uri baseUri) : base(baseUri)
    {
        this.OperationUrlPrefix += this.GetType().Name.Replace("Client", "/").ToLower();
    }

    protected BaseClient(string baseUrl) : this(new Uri(baseUrl))
    { }

    protected virtual string PrepareOperationUrl(string operationUrl)
    {
        return operationUrl.StartsWith(this.OperationUrlPrefix) ? operationUrl : this.OperationUrlPrefix + operationUrl;
    }

    protected virtual Parameter BuildParameter(string name, object value, ParameterType parameterType, string contentType = null, DataFormat? dataFormat = null)
    {
        return contentType == null ? new Parameter(name, value, parameterType)
                                   : new Parameter(name, value, contentType, parameterType);
    }

    protected virtual T Get<T>(IRestRequest restRequest)
             where T : class, new()
    {
        restRequest.Method = Method.GET;
        var response = base.Execute<T>(restRequest);
        if (response.StatusCode == HttpStatusCode.OK)
            return response.Data;

        // this.LogError(base.BaseUrl, restRequest, response);


        if (response.StatusCode == HttpStatusCode.Unauthorized)
            throw new UnauthorizedAccessException(response.Content);

        return default(T);
    }

    protected virtual T Get<T>(string operationUrl, Dictionary<string, object> urlSegments)
            where T : class, new()
    {
        operationUrl = this.PrepareOperationUrl(operationUrl);
        var restRequest = new RestRequest(operationUrl);
        if (urlSegments?.Count > 0)
        {
            foreach (var urlSegment in urlSegments)
                restRequest.AddUrlSegment(urlSegment.Key, urlSegment.Value);
        }

        return this.Get<T>(restRequest);
    }

    protected virtual T Get<T>(string operationUrl, List<Parameter> parameters, Dictionary<string, string> headerValues = null)
        where T : class, new()
    {
        operationUrl = this.PrepareOperationUrl(operationUrl);
        var restRequest = new RestRequest(operationUrl);

        if (headerValues?.Count > 0)
        {
            foreach (var headerValue in headerValues)
                restRequest.AddHeader(headerValue.Key, headerValue.Value);
        }

        if (parameters?.Count > 0)
            restRequest.Parameters.AddRange(parameters);

        return this.Get<T>(restRequest);
    }





}
