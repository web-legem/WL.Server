using LanguageExt;
using System.IO;

namespace WL.Application.Users.Queries {

  public class DownloadPhotoQuery {

    public Try<FileStream> Execute(long id, bool mode) {
      var fileStream = UserHelpers.GetFile(id, mode);

      if (fileStream != null) {
        return () => fileStream;
      }

      return null;
    }
  }
}