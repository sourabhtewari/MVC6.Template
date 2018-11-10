using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [ExcludeFromCodeCoverage]
    public class InheritedAuthorizedController : AuthorizedController
    {
        [HttpGet]
        public ViewResult InheritanceAction()
        {
            return null;
        }
    }
}
