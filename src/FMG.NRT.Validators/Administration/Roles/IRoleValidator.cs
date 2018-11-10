using FMG.NRT.Objects;
using System;

namespace FMG.NRT.Validators
{
    public interface IRoleValidator : IValidator
    {
        Boolean CanCreate(RoleView view);
        Boolean CanEdit(RoleView view);
    }
}
