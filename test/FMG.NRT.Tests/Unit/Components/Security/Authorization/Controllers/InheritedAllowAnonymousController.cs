using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [ExcludeFromCodeCoverage]
    public class InheritedAllowAnonymousController : AllowAnonymousController
    {
        [HttpGet]
        public ViewResult InheritanceAction()
        {
            return null;
        }
    }
}
