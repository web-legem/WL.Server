using LanguageExt;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using WL.Domain.User;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Users.UserValidations;

namespace WL.Application.Users.Commands {

  public class CreateUserCommandHandler {
    private readonly IUserRepository _uRepository;

    public CreateUserCommandHandler(IUserRepository uRepository) {
      _uRepository = uRepository;
    }

    public Try<Validation<Error, UserDto>> Execute(CreateUserCmd msg, Stream stream)
       => ()
       => from x in ValidateCreateUserMsg(msg)
          let user = CreatePassToUser(x.ToUser())
          let y = _uRepository.Create(user)
          let z = SavePhoto(stream, y)
          select y.ToUserDTO();

    User CreatePassToUser(User user) {
      MD5 md5provider = new MD5CryptoServiceProvider();

      byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(user.IDDocument));
      var sb = new StringBuilder();
      for (var i = 0; i < bytes.Length; i++) {
        sb.Append(bytes[i].ToString("x2"));
      }
      user.Password = sb.ToString();
      return user;
    }

    bool SavePhoto(Stream stream, User user) {
      if (stream != null) {
        UserHelpers.SaveFile(user.Id + ".png", stream);
        return true;
      }
      return false;
    }

    Validation<Error, CreateUserCmd> ValidateCreateUserMsg(CreateUserCmd msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateFirstName(x.FirstName),
                ValidateLastName(x.LastName),
                ValidateDocument(x.Document),
                ValidateNickname(x.Nickname),
                validatePassword(x.Email),
                ValidateRoleId(x.RoleId)
             )
             .Apply((X1, X2, X3, X4, X5, X6) => msg)
          select y;
  }
}