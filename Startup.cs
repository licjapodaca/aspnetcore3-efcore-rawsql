using System;
using AutoMapper;
using EFCore3.Context;
using EFCore3.Repositories.Contracts;
using EFCore3.Repositories.Implementations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace EFCore3
{
	public class Startup
    {
		private readonly IConfiguration _config;
		private string _connString;

		public Startup(IWebHostEnvironment env)
		{
			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional : false, reloadOnChange : true)
				.AddJsonFile($"appsettings.{env.EnvironmentName.ToLower()}.json", optional : false, reloadOnChange : true)
				.AddEnvironmentVariables();

			if (env.IsDevelopment())
			{
				builder.AddUserSecrets<Startup>();
			}

			_config = builder.Build();
		}

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
			var builder = new SqlConnectionStringBuilder(_config.GetConnectionString("DefaultConnection"));

			builder.UserID = _config.GetValue<string>("Database:DbUserId");
			builder.Password = _config.GetValue<string>("Database:DbPassword");

			_connString = builder.ConnectionString;

			services.AddDbContextPool<MyDbContext>(options =>
				{
					options.UseSqlServer(_connString, sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 10,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
					});
				},
				poolSize : 100);

			services.AddDbContextPool<MyDbContextReadOnly>(options =>
				{
					options.UseSqlServer(_connString, sqlOptions =>
					{
						sqlOptions.EnableRetryOnFailure(
							maxRetryCount: 10,
							maxRetryDelay: TimeSpan.FromSeconds(30),
							errorNumbersToAdd: null);
					});
				},
				poolSize : 100);

			services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

			services.AddScoped<IConsultaRepository, ConsultaRepository>();

            services
				.AddControllers()
				.AddNewtonsoftJson(options =>
				{
					options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
					options.SerializerSettings.ContractResolver = new DefaultContractResolver();
				});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
