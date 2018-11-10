using FMG.NRT.Resources.Form;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace FMG.NRT.Components.Mvc
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class IntegerAttribute : ValidationAttribute
    {
        public IntegerAttribute()
            : base(() => Validations.Integer)
        {
        }

        public override Boolean IsValid(Object value)
        {
            return value == null || Regex.IsMatch(value.ToString(), "^[+-]?[0-9]+$");
        }
    }
}
