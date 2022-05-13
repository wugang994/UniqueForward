using Microsoft.AspNetCore.Rewrite;
using Reader.Middleware;
using Reader.Proxy;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient<ProxyHttpClient>()
           .ConfigurePrimaryHttpMessageHandler(x => new HttpClientHandler()
           {
               AllowAutoRedirect = false,
               MaxConnectionsPerServer = int.MaxValue,
               UseCookies = false,
           }); //注入我们定义的HttpClient
builder.Services.AddSingleton<IUrlRewriter, PrefixRewriter>();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ProxyMiddleware>();

app.Run();
