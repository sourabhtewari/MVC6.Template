using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Security;
using FMG.NRT.Services;

namespace FMG.NRT.Controllers
{
    [AllowUnauthorized]
    public class HomeController : ServicedController<IAccountService>
    {
        public HomeController(IAccountService service)
            : base(service)
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            if (!Service.IsActive(CurrentAccountId))
                return RedirectToAction("Logout", "Auth");

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult Error()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public new ViewResult NotFound()
        {
            return View();
        }

        [HttpGet]
        public new ActionResult Unauthorized()
        {
            if (!Service.IsActive(CurrentAccountId))
                return RedirectToAction("Logout", "Auth");

            return View();
        }
    }
}
