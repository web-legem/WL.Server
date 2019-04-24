using System;
using LanguageExt;
using WL.Application.Common;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Roles.RoleValidations;

namespace WL.Application.Roles.Commands {

  public class CreateRoleCommandHandler {
    private readonly IRoleRepository _rRepository;

    public CreateRoleCommandHandler(IRoleRepository uRepository) {
      _rRepository = uRepository;
    }

    public Try<Validation<Error, RoleDto>> Execute(CreateRoleCommand msg)
       => ()
       => from x in ValidateCreateRoleMsg(msg)
          let y = _rRepository.Create(x.ToRole())
          select y.ToRoleDTO();

    Validation<Error, CreateRoleCommand> ValidateCreateRoleMsg(CreateRoleCommand msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateName(x.Name),
                ValidateConfigSystem(x.ConfigSystem),
                ValidateCreateDocuments(x.CreateDocuments),
                ValidateDeleteDocuments(x.DeleteDocuments))
             .Apply((X1, X2, X3, X4) => msg)
          select y;
  }
}