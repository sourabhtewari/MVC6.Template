﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using FMG.NRT.Resources.Form;
using System;

namespace FMG.NRT.Components.Mvc
{
    public static class ModelMessagesProvider
    {
        public static void Set(DefaultModelBindingMessageProvider messages)
        {
            messages.SetAttemptedValueIsInvalidAccessor((value, field) => String.Format(Validations.InvalidField, field));
            messages.SetUnknownValueIsInvalidAccessor(field => String.Format(Validations.InvalidField, field));
            messages.SetMissingBindRequiredValueAccessor(field => String.Format(Validations.Required, field));
            messages.SetValueMustNotBeNullAccessor(field => String.Format(Validations.Required, field));
            messages.SetValueIsInvalidAccessor(value => String.Format(Validations.InvalidValue, value));
            messages.SetValueMustBeANumberAccessor(field => String.Format(Validations.Numeric, field));
            messages.SetMissingKeyOrValueAccessor(() => Validations.RequiredValue);
        }
    }
}
