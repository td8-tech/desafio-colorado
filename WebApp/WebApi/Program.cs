using Microsoft.AspNetCore.Hosting;
using WebApi;

var builder = WebApplication.CreateBuilder(args);


var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services, builder.Environment);


var app = builder.Build();

startup.Configure(app);

app.Run();
