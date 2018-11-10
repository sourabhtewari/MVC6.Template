using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Mvc;
using FMG.NRT.Objects;
using FMG.NRT.Services;
using FMG.NRT.Validators;
using System;

namespace FMG.NRT.Controllers.Administration
{
    [Area("Administration")]
    public class AccountsController : ValidatedController<IAccountValidator, IAccountService>
    {
        public AccountsController(IAccountValidator validator, IAccountService service)
            : base(validator, service)
        {
        }

        [HttpGet]
        public ViewResult Index()
        {
            return View(Service.GetViews());
        }

        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create([BindExcludeId] AccountCreateView account)
        {
            if (!Validator.CanCreate(account))
                return View(account);

            Service.Create(account);

            Authorization?.Refresh();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(Int32 id)
        {
            return NotEmptyView(Service.Get<AccountView>(id));
        }

        [HttpGet]
        public ActionResult Edit(Int32 id)
        {
            return NotEmptyView(Service.Get<AccountEditView>(id));
        }

        [HttpPost]
        public ActionResult Edit(AccountEditView account)
        {
            if (!Validator.CanEdit(account))
                return View(account);

            Service.Edit(account);

            Authorization?.Refresh();

            return RedirectToAction("Index");
        }
    }
}
