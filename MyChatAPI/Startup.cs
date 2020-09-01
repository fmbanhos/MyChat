using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MyChatAPI.Domain.Repositories;
using MyChatAPI.Infra;
using MyChatAPI.Infra.Helpers;

namespace MyChatAPI
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
			services.AddControllers();
			services.AddMediatR(typeof(Startup));
			services.AddMvc();

			#region Repository
			services.AddSingleton<IGroupRepository, GroupRepository>();
			services.AddSingleton<IPersonRepository, PersonRepository>();
			services.AddSingleton<IMessageRepository, MessageRepository>();
			#endregion

			#region AutoMapper
			MapperConfiguration mapperConfig = new MapperConfiguration(a =>	{ a.AddProfile(new MappingProfile()); });
			IMapper mapper = mapperConfig.CreateMapper();
			services.AddSingleton(mapper);
			#endregion

			#region Swagger
			services.AddSwaggerGen(a => 
			{
				a.SwaggerDoc(SwaggerHelper.Version, SwaggerHelper.GetOpenApiInfo());
				//a.IncludeXmlComments(SwaggerHelper.GetXmlComments());
			});
			#endregion
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

			#region Swagger
			// Ativando middlewares para uso do Swagger
			app.UseSwagger();
			app.UseSwaggerUI(a => 
			{
				a.SwaggerEndpoint(SwaggerHelper.EndPointUrl, $"{SwaggerHelper.Title} {SwaggerHelper.Version.ToUpper()}");
				a.RoutePrefix = string.Empty;
			});
			#endregion

			//app.UseMvc();
		}
	}
}
