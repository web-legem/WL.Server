using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using WL.Application.DocumentTypes.Commands;
using WL.Application.DocumentTypes.Queries;
using WL.Application.Interfaces.Persistance;
using WL.Persistance;

namespace WL.Api {

  public class Startup {

    public void ConfigureServices(IServiceCollection services) {
      services.AddMvc();
      services.AddCors();

      services.AddDbContext<WLDbContext>(options => {
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
  }
}