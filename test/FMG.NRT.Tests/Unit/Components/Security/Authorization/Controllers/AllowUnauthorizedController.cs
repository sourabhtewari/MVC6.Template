using FMG.NRT.Components.Security;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [AllowUnauthorized]
    [ExcludeFromCodeCoverage]
    public class AllowUnauthorizedController : AuthorizedController
    {
    }
}
