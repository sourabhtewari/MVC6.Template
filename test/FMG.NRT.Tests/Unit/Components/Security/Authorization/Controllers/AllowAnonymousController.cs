using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Security;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [AllowAnonymous]
    [ExcludeFromCodeCoverage]
    public class AllowAnonymousController : AuthorizedController
    {
        [HttpGet]
        [Authorize]
        [AllowAnonymous]
        [AllowUnauthorized]
        public ViewResult AuthorizedAction()
        {
            return null;
        }
    }
}
