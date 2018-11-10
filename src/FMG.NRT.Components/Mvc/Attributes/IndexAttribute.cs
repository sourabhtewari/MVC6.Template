using System;

namespace FMG.NRT.Components.Mvc
{
    [AttributeUsage(AttributeTargets.Property)]
    public class IndexAttribute : Attribute
    {
        public Boolean IsUnique { get; set; }
    }
}
