using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WL.Application.DocumentTypes.Commands;
using WL.Application.DocumentTypes.Queries;
using WL.Application.Interfaces.Persistance;
using WL.Persistance;
using WL.Persistance.DocumentTypes;

namespace WL.Api {

  public class Startup {

    public Startup(IConfiguration configuration) {
      Configuration = configuration;
      ConfigureAppDirectories();
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc();
      services.AddCors();

      var connectionString = Configuration["ConnectionStrings:DefaultConnection"];
      services.AddDbContext<WLDbContext>(options => {
        options.UseOracle(connectionString, b => b.MigrationsAssembly("WL.Persistance"));
      });

      services.AddScoped<IDocumentTypeRepository, DocumentTypeRepository>();
      services.AddScoped<GetDocumentTypeQuery>();
      services.AddScoped<GetAllDocumentTypesQuery>();
      services.AddScoped<CreateDocumentTypeCommandHandler>();
      services.AddScoped<UpdateDocumentTypeCommandHandler>();
      services.AddScoped<DeleteDocumentTypeCommandHandler>();
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
      // TODO - get the directories from appsettings.json
      var root = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
      var documentDir = Path.Combine(root, "documents");
      var photoDir = Path.Combine(root, "users", "photos");
      var thumbnailDir = Path.Combine(root, "users", "thumbnails");

      if (!Directory.Exists(documentDir)) {
        Directory.CreateDirectory(documentDir);
      }
      if (!Directory.Exists(photoDir)) {
        Directory.CreateDirectory(photoDir);
      }
      if (!Directory.Exists(thumbnailDir)) {
        Directory.CreateDirectory(thumbnailDir);
      }
    }
  }
}