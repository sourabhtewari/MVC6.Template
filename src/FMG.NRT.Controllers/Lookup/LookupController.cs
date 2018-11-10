using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Lookups;
using FMG.NRT.Components.Mvc;
using FMG.NRT.Components.Security;
using FMG.NRT.Data.Core;
using FMG.NRT.Objects;
using NonFactors.Mvc.Lookup;
using System;

namespace FMG.NRT.Controllers
{
    [AllowUnauthorized]
    public class LookupController : BaseController
    {
        private IUnitOfWork UnitOfWork { get; }

        public LookupController(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }

        [NonAction]
        public virtual JsonResult GetData(MvcLookup lookup, LookupFilter filter)
        {
            lookup.Filter = filter;

            return Json(lookup.GetData());
        }

        [AjaxOnly]
        public JsonResult Role(LookupFilter filter)
        {
            return GetData(new MvcLookup<Role, RoleView>(UnitOfWork), filter);
        }

        protected override void Dispose(Boolean disposing)
        {
            UnitOfWork.Dispose();

            base.Dispose(disposing);
        }
    }
}
