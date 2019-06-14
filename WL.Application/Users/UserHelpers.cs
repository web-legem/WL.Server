using System.IO;
using WL.Application.Users.Commands;
using System.Drawing;
using System;
using static WL.Application.Helpers.DirectoryHelpers;

namespace WL.Application.Users {

  public static class UserHelpers {

    public static Domain.User.User ToUser(this CreateUserCmd res)
       => new Domain.User.User {
         Nickname = res.Nickname,
         FirstName = res.FirstName,
         LastName = res.LastName,
         IDDocument = res.Document,
         Password = res.Password,
         Email = res.Email,
         RoleId = res.RoleId,
         State = res.State,
         EntityId = res.EntityId
       };

    public static Domain.User.User ToUser(this UpdateUserCmd res)
       => new Domain.User.User {
         Id = res.Id,
         Nickname = res.Nickname,
         FirstName = res.FirstName,
         LastName = res.LastName,
         IDDocument = res.Document,
         Password = res.Password,
         Email = res.Email,
         State = res.State,
         RoleId = res.RoleId,
         EntityId = res.EntityId
       };

    public static UserDto ToUserDTO(this Domain.User.User res)
       => new UserDto {
         Id = res.Id,
         Nickname = res.Nickname,
         FirstName = res.FirstName,
         LastName = res.LastName,
         Document = res.IDDocument,
         Password = res.Password,
         Email = res.Email,
         State = res.State,
         RoleId = res.RoleId,
         EntityId = res.EntityId
       };

    public static void SaveFile(string fileName, Stream stream) {
      var photosDir = GetPhotosDirectory();
      var thumbnailDir = GetThumbnailsDirectory();

      var filePathNor = new FileInfo(Path.Combine(photosDir, fileName));
      var filePathMin = new FileInfo(Path.Combine(thumbnailDir, fileName));

      if (File.Exists(filePathNor.FullName)) {
        filePathNor.Delete();
      }
      if (File.Exists(filePathMin.FullName)) {
        filePathMin.Delete();
      }

      using (var output = new FileStream(filePathNor.FullName, FileMode.Create)) {
        stream.CopyTo(output);
      }

      Image image;
      using (var bmpTemp = new Bitmap(filePathNor.FullName)) {
        image = new Bitmap(bmpTemp);
      }
      var thumb = image.GetThumbnailImage(62, 80, () => false, IntPtr.Zero);
      thumb.Save(filePathMin.FullName);
    }

    public static Boolean existThumbnail(long id) {
      var thumbnailDir = GetThumbnailsDirectory();
      var filePathMin = new FileInfo(Path.Combine(thumbnailDir, id + ".png"));
      if (File.Exists(filePathMin.FullName)) {
        return true;
      }
      return false;
    }

    public static void DeleteFile(long id) {
      var photoDir = GetPhotosDirectory();
      var thumbnailDir = GetThumbnailsDirectory();

      var filePathNor = new FileInfo(Path.Combine(photoDir, id + ".png"));
      var filePathMin = new FileInfo(Path.Combine(thumbnailDir, id + ".png"));

      if (File.Exists(filePathNor.FullName)) {
        filePathNor.Delete();
      }
      if (File.Exists(filePathMin.FullName)) {
        filePathMin.Delete();
      }
    }

    public static FileStream GetFile(long id, bool mode) {
      var photoDir = GetPhotosDirectory();
      var thumbnailDir = GetPhotosDirectory();

      var filePathNor = new FileInfo(Path.Combine(photoDir, id + ".png"));
      var filePathMin = new FileInfo(Path.Combine(thumbnailDir, id + ".png"));

      var fullPath = (mode ? filePathMin.FullName : filePathNor.FullName);

      if (File.Exists(fullPath)) {
        return new FileStream(fullPath, FileMode.Open);
      }
      return null;
    }
  }
}