using System;

namespace FMG.NRT.Components.Security
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AllowUnauthorizedAttribute : Attribute
    {
    }
}
