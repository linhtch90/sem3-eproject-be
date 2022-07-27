using allu_decor_be.Authorization;
using allu_decor_be.Helpers;
using allu_decor_be.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace allu_decor_be
{
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
            services.AddDbContext<DataContext>();
            services.AddCors();

            //services.AddControllers();
            services.AddControllers().AddJsonOptions(x =>
            {
                // serialize enums as strings in api responses (e.g. Role)
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "allu_decor_be", Version = "v1" });
            });

            // configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // configure DI for application services
            services.AddScoped<IJwtUtils, JwtUtils>();
            services.AddScoped<IUserService, UserService>();
            // service for Domain
            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<IServiceService, ServiceService>();
            // service for Domainservice
            services.AddScoped<IDomainserviceService, DomainserviceService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IFeedbackSevice, FeedbackService>();
            services.AddScoped<IFaqService, FaqService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IContactinfoService, ContactinfoService>();
            //service for CustomerReviewService
            services.AddScoped<ICustomerReviewService, CustomerReviewService>();
            services.AddScoped<IAboutuService, AboutuService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IInvoiceitemService, InvoiceitemService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "allu_decor_be v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            // global error handler
            app.UseMiddleware<ErrorHandlerMiddleware>();

            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();

            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
