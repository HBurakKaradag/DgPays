using System.Net;
using DgPays.API.Middlewares;
using DgPays.API.Services;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => { options.Listen(IPAddress.Any, 80); });

builder.Services.AddTransient<IProductService, ProductService>();

 builder.Services.AddControllers();
 
//                 .AddNewtonsoftJson(option => option.SerializerSettings.ContractResolver = new DefaultContractResolver
//                 {
//                     NamingStrategy = new CustomerNameStrategy()
//                 });

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "DgPays.API", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DgPays.API v1"));
}

app.UseMiddleware<RequestResponseMiddleware>();


//app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();
