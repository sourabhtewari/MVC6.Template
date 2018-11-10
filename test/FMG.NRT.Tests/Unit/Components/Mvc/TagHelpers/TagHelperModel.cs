using System;
using System.ComponentModel.DataAnnotations;

namespace FMG.NRT.Tests.Unit.Components.Mvc
{
    public class TagHelperModel
    {
        [Required]
        public String Required { get; set; }
        public String NotRequired { get; set; }
        public Int64 RequiredValue { get; set; }
        public Int64? NotRequiredNullableValue { get; set; }
    }
}
