using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Components.Mvc
{
    public class RequiredAdapter : RequiredAttributeAdapter
    {
        public RequiredAdapter(RequiredAttribute attribute)
            : base(attribute, null)
        {
        }

        public override String GetErrorMessage(ModelValidationContextBase context)
        {
            Attribute.ErrorMessage = Validations.Required;

            return GetErrorMessage(context.ModelMetadata);
        }
    }
}
