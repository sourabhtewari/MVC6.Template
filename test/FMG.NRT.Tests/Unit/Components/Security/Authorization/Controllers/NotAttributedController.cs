using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [ExcludeFromCodeCoverage]
    public class NotAttributedController : Controller
    {
        [HttpGet]
        public ViewResult Action()
        {
            return null;
        }
    }
}
