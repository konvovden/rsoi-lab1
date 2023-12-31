using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PersonService.Core.Repositories;
using PersonService.Database.Context;
using PersonService.Database.Repositories;

namespace PersonService.Server;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "PersonService.Server", Version = "v1" });
        });
        services.AddSwaggerGenNewtonsoftSupport();
        
        services.AddDbContext<PersonServiceContext>(opt => 
            opt.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IPersonRepository, PersonRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PersonService.Server v1"));
        
        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}