using Microsoft.AspNetCore.Mvc.DataAnnotations.Internal;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Components.Mvc
{
    public class RangeAdapter : RangeAttributeAdapter
    {
        public RangeAdapter(RangeAttribute attribute)
            : base(attribute, null)
        {
        }

        public override String GetErrorMessage(ModelValidationContextBase context)
        {
            Attribute.ErrorMessage = Validations.Range;

            return GetErrorMessage(context.ModelMetadata);
        }
    }
}
