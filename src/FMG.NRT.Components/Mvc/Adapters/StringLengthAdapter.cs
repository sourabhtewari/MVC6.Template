using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Components.Mvc
{
    public class StringLengthAdapter : StringLengthAttributeAdapter
    {
        public StringLengthAdapter(StringLengthAttribute attribute)
            : base(attribute, null)
        {
        }

        public override String GetErrorMessage(ModelValidationContextBase context)
        {
            Attribute.ErrorMessage = Attribute.MinimumLength == 0 ? Validations.StringLength : Validations.StringLengthRange;

            return GetErrorMessage(context.ModelMetadata);
        }
    }
}
