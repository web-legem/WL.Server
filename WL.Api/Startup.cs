using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System;
using System.IO;

using WL.Api.Infrastructure;
using WL.Persistance;

using static WL.Application.Helpers.DirectoryHelpers;

namespace WL.Api {

  public class Startup {

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
      ConfigureAppDirectories();
    }

    public IConfiguration Configuration { get; }

    public IServiceProvider ConfigureServices(IServiceCollection services) {
      services.AddMvc(options => {
        //options.ModelValidatorProviders.Clear();
        options.Filters.Add(typeof(AuthFilter));
      });
      services.AddCors();

      var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      services.AddDbContext<WLDbContext>(options => {
        options.UseOracle(connectionString, b => b.MigrationsAssembly("WL.Persistance"));
      });

      //services.Configure<ApiBehaviorOptions>(options => {
      //  options.SuppressModelStateInvalidFilter = true;
      //});

      var containerBuilder = new ContainerBuilder();
      containerBuilder.RegisterModule<AutofacModule>();
      containerBuilder.Populate(services);
      var container = containerBuilder.Build();
      return new AutofacServiceProvider(container);
    }

    public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
      if (env.IsDevelopment()) {
        app.UseDeveloperExceptionPage();
      }

      app.UseCors(builder => builder.WithOrigins("http://localhost:3000")
        .AllowAnyHeader()
        .AllowAnyMethod()
      );

      app.UseMvc();
    }

    void ConfigureAppDirectories() {
      var baseDirectory = GetBaseDirectory();

      if (!Directory.Exists(baseDirectory))
        Directory.CreateDirectory(baseDirectory);

      var documentDir = GetDocumentsDirectory();
      var photoDir = GetPhotosDirectory();
      var thumbnailDir = GetThumbnailsDirectory();
      var textDir = GetTextDirectory();

      if (!Directory.Exists(documentDir))
        Directory.CreateDirectory(documentDir);

      if (!Directory.Exists(photoDir))
        Directory.CreateDirectory(photoDir);

      if (!Directory.Exists(thumbnailDir))
        Directory.CreateDirectory(thumbnailDir);

      if (!Directory.Exists(textDir))
        Directory.CreateDirectory(textDir);
    }
  }
}