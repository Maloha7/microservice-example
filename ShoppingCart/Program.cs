using ShoppingCart.ShoppingCart;
using Polly;
using Polly.Extensions.Http;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.Scan(selector =>
        selector
            .FromAssemblyOf<Program>()
            .AddClasses(c => c.AssignableTo<IService>())
            .AsImplementedInterfaces());


builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
    .AddPolicyHandler(HttpPolicyExtensions
        .HandleTransientHttpError()
        .WaitAndRetryAsync(
            3, // Retry 3 times
            attempt => TimeSpan.FromMilliseconds(100 * Math.Pow(2, attempt)) // Exponential backoff
        ));


var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();

app.Run();
