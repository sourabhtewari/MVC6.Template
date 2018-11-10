﻿using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Components.Mvc
{
    public class EmailAddressAdapter : AttributeAdapterBase<EmailAddressAttribute>
    {
        public EmailAddressAdapter(EmailAddressAttribute attribute)
            : base(attribute, null)
        {
        }

        public override void AddValidation(ClientModelValidationContext context)
        {
            context.Attributes["data-val"] = "true";
            context.Attributes["data-val-email"] = GetErrorMessage(context);
        }
        public override String GetErrorMessage(ModelValidationContextBase context)
        {
            Attribute.ErrorMessage = Validations.Email;

            return GetErrorMessage(context.ModelMetadata);
        }
    }
}
