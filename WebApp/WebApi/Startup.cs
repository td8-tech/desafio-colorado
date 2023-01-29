using Business.Repository;
using Business.Repository.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Services.Services;
using Services.Services.Interfaces;


namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services, IWebHostEnvironment env)
        {
            services.AddControllers();
            services.AddHealthChecks();
            
            services.AddScoped<IClientRepository, ClientRepository>();
            services.AddScoped<IClientService, ClientService>();

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new OpenApiInfo
                {
                    Title = "Cliente",
                    Description = "Api",
                });

                options.CustomSchemaIds(x => x.FullName); //Essa linha deve ser inserida em casos que há classes com mesmo nome em namespaces diferentes

                //Obtendo o diretório e depois o nome do arquivo .xml de comentários
                var caminhoAplicacao = env.ContentRootPath;
                var nomeAplicacao = env.ApplicationName;
                var caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");

                //Caso exista arquivo então adiciona-lo
                if (File.Exists(caminhoXmlDoc))
                {
                    options.IncludeXmlComments(caminhoXmlDoc);
                }

                options.EnableAnnotations();               

            });

            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient, ServiceLifetime.Transient);
            
            services.AddMvc();

            services.AddCors(options =>
            {
                options.AddPolicy(name: "MyPolicy",
                    policy =>
                    {
                        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                    });
            });

            services.Configure<RouteOptions>(options => options.LowercaseUrls = true);

        }

        public void Configure(WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger(c =>
                {
                    c.RouteTemplate = "/api/swagger/{documentName}/swagger.json";
                });

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint(url: "/api/swagger/v2/swagger.json", "Client API");
                    c.RoutePrefix = "api/swagger";
                });

                app.UseCors("MyPolicy");
            }

            app.UseAuthorization();

            app.MapControllers();
        }

    }
}
