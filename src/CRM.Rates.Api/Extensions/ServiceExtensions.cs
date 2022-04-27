using CRM.Rates.WebApi.Interface;
using CRM.Rates.WebApi.Repository;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.DependencyInjection;

namespace CRM.Rates.WebApi.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        public static void ConfigureTransient(this IServiceCollection services)
        {
            services.AddTransient<IRate, RateRepository>();
            services.AddTransient<IRateClasses, RateClassesRepository>();
            services.AddTransient<IRateDirection, RateDirectionRepository>();
            services.AddTransient<ICustomerUniqueRate, CustomerUniqueRateRepository>();
            services.AddTransient<ICustomerAnalysis, CustomerAnalysisRepository>();
            services.AddTransient<IRateIncrease, RateIncreaseRepository>();
            services.AddTransient<IRateCycle, RateCycleRepository>();
            services.AddTransient<IWorkFlow, WorkFlowRepository>();
            services.AddTransient<IUploadDocument, UploadDocumentRepository>();
            services.AddTransient<IIncreaseDocument, IncreaseDocumentRepository>();
            services.AddTransient<IRepTarget, RepTargetRepository>();
            services.AddTransient<IBranch, BranchRepository>();
        }

        public static void ConfigureOutputFormatters(this IServiceCollection services)
        {
            services.AddControllers(opt => // or AddMvc()
            {
                // remove formatter that turns nulls into 204 - No Content responses
                // this formatter breaks Angular's Http response JSON parsing
                opt.OutputFormatters.RemoveType<HttpNoContentOutputFormatter>();
            });
        }
    }
}
