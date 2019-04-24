using Microsoft.Extensions.Configuration;
using System.IO;

namespace WL.Application.Helpers {

  public static class DirectoryHelpers {

    public static string GetBaseDirectory() {
      var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

      return configuration["BaseDirectory"];
    }

    public static string GetDocumentsDirectory() {
      return Path.Combine(GetBaseDirectory(), "documents");
    }

    public static string GetPhotosDirectory() {
      return Path.Combine(GetBaseDirectory(), "users", "photos");
    }

    public static string GetThumbnailsDirectory() {
      return Path.Combine(GetBaseDirectory(), "users", "thumbnails");
    }

    public static string GetTextDirectory() {
      return Path.Combine(GetBaseDirectory(), "text");
    }
  }
}