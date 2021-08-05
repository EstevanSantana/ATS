using ATS.BackEnd.Domain.Interfaces;
using ATS.BackEnd.Infrastructure.Data.ContextConfig;
using ATS.BackEnd.Infrastructure.Data.Repository;
using ATS.BackEnd.Service.Interfaces;
using ATS.BackEnd.Service.Services;
using ATS.BackEnd.Service.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ATS.BackEnd.WebAPI.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection DependencyInjection(this IServiceCollection services,
                      IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                    options.UseSqlite(configuration.GetConnectionString("DefaultConnection"),
                    x => x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<ICandidatoRepository, CandidatoRepository>();
            services.AddScoped<ICandidatoService, CandidatoService>();
            services.AddScoped<IErrorMessageService, ErrorMessageService>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddScoped<ICurriculoRepository, CurriculoRepository>();

            services.AddAutoMapper(typeof(Startup).Assembly);

            return services;
        }
    }
}
