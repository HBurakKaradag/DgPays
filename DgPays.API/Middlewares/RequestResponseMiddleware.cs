using System;
using DgPays.API.Publishers;
using Microsoft.IO;

namespace DgPays.API.Middlewares
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;


        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
        }

        public async Task Invoke(HttpContext context)
        {
            await RequestPipeLine(context);
            await ResponsePipeLine(context);
        }


        private async Task RequestPipeLine(HttpContext context)
        {

            context.Request.EnableBuffering();
            await using var requestStream = _recyclableMemoryStreamManager.GetStream();
            await context.Request.Body.CopyToAsync(requestStream);

            var requestBody = ReadRequestStream(requestStream);

            /*
                 https://www.dgpays.com/Product?productId=1004&status=3;

                 https           : Schema
                 www.dgpays.com  : Host
                 Product         : AbsolutePath
                 ?producId=1004. : QueryString
            */

            if(!string.IsNullOrWhiteSpace(requestBody))
            {
                context.Request.Headers.TryGetValue("X-Correlation-id", out var correlationId);
                var requestCorrelationId = correlationId.FirstOrDefault() ?? Guid.NewGuid().ToString();

                await RequestResponsePublisher.Publish(new Domain.LogModel{ Body = requestBody, RouteKey = "RequestLogEvent" });
            }

            context.Request.Body.Position = 0;
        }

        private async Task ResponsePipeLine(HttpContext context)
        {
            var originalbodyStream = context.Response.Body;
           
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0,SeekOrigin.Begin);
            var responseBodyStr = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0,SeekOrigin.Begin);

            if(!string.IsNullOrWhiteSpace(responseBodyStr))
            {
                await RequestResponsePublisher.Publish(new Domain.LogModel { Body = responseBodyStr, RouteKey = "ResponseLogEvent" });
            }

            await responseBody.CopyToAsync(originalbodyStream);

        }


        private string ReadRequestStream(Stream stream)
        {
            const int readChunkSize = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);

            var readChunk = new char[readChunkSize];
            int chunkLength;

            do
            {
                chunkLength = reader.ReadBlock(readChunk, 0, readChunkSize);
                textWriter.Write(readChunk, 0, chunkLength);
            } while (chunkLength > 0);

            return textWriter.ToString();

        }

    }
}
