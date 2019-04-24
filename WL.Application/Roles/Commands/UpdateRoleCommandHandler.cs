using LanguageExt;
using WL.Application.Common.Errors;
using WL.Application.Interfaces.Persistance;
using static LanguageExt.Prelude;
using static WL.Application.Common.CommonValidations;
using static WL.Application.Common.FormValidations;
using static WL.Application.Roles.RoleValidations;

namespace WL.Application.Roles.Commands {

  public class UpdateRoleCommandHandler {
    readonly IRoleRepository _rRepository;

    public UpdateRoleCommandHandler(IRoleRepository _rRepository) {
      this._rRepository = _rRepository;
    }

    public Try<Validation<Error, RoleDto>> Execute(UpdateRoleCommand msg)
       => ()
       => from x in ValidateUpdateMsg(msg) // validaciones antes
          let y = x.ToRole()
          let z = _rRepository.Update(y) // otras funciones despues
          select z.ToRoleDTO();

    Validation<Error, UpdateRoleCommand> ValidateUpdateMsg(UpdateRoleCommand msg)
       => from x in ValidateNonNull(msg)
          from y in (
                ValidateId(x.Id),
                ValidateName(x.Name),
                ValidateConfigSystem(x.ConfigSystem),
                ValidateCreateDocuments(x.CreateDocuments),
                ValidateDeleteDocuments(x.DeleteDocuments))
            .Apply((id, x1, x2, x3, x4) => msg)
          select y;
  }
}