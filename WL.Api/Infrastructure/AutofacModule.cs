using Autofac;

using System.Linq;

using WL.Application.Interfaces.Persistance;
using WL.Persistance;

namespace WL.Api.Infrastructure {

  public class AutofacModule : Module {

    protected override void Load(ContainerBuilder builder) {
      builder.RegisterAssemblyTypes(typeof(WLDbContext).Assembly)
        .Where(c => c.Name.EndsWith("Repository"))
        .AsImplementedInterfaces();

      builder.RegisterAssemblyTypes(typeof(IDocumentTypeRepository).Assembly)
        .Where(c => c.Name.EndsWith("Handler") || c.Name.EndsWith("Query"))
        .AsSelf();
    }
  }
}