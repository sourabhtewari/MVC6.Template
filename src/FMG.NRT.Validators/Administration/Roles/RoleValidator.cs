﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using FMG.NRT.Data.Core;
using FMG.NRT.Objects;
using FMG.NRT.Resources.Views.Administration.Roles.RoleView;
using System;
using System.Linq;

namespace FMG.NRT.Validators
{
    public class RoleValidator : BaseValidator, IRoleValidator
    {
        public RoleValidator(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean CanCreate(RoleView view)
        {
            Boolean isValid = ModelState.IsValid;
            isValid &= IsUniqueTitle(view);

            return isValid;
        }
        public Boolean CanEdit(RoleView view)
        {
            Boolean isValid = ModelState.IsValid;
            isValid &= IsUniqueTitle(view);

            return isValid;
        }

        private Boolean IsUniqueTitle(RoleView view)
        {
            Boolean isUnique = !UnitOfWork
                .Select<Role>()
                .Any(role =>
                    role.Id != view.Id &&
                    role.Title.ToLower() == (view.Title ?? "").ToLower());

            if (!isUnique)
                ModelState.AddModelError<RoleView>(role => role.Title, Validations.UniqueTitle);

            return isUnique;
        }
    }
}
