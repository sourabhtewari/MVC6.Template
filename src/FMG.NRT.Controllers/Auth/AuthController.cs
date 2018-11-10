﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Mail;
using FMG.NRT.Objects;
using FMG.NRT.Resources.Views.Administration.Accounts.AccountView;
using FMG.NRT.Services;
using FMG.NRT.Validators;
using System;
using System.Threading.Tasks;

namespace FMG.NRT.Controllers
{
    [AllowAnonymous]
    public class AuthController : ValidatedController<IAccountValidator, IAccountService>
    {
        public IMailClient MailClient { get; }

        public AuthController(IAccountValidator validator, IAccountService service, IMailClient mailClient)
            : base(validator, service)
        {
            MailClient = mailClient;
        }

        [HttpGet]
        public ActionResult Recover()
        {
            if (Service.IsLoggedIn(User))
                return RedirectToDefault();

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Recover(AccountRecoveryView account)
        {
            if (Service.IsLoggedIn(User))
                return RedirectToDefault();

            if (!Validator.CanRecover(account))
                return View(account);

            String token = Service.Recover(account);
            if (token != null)
            {
                String url = Url.Action("Reset", "Auth", new { token }, Request.Scheme);

                await MailClient.SendAsync(
                    account.Email,
                    Messages.RecoveryEmailSubject,
                    String.Format(Messages.RecoveryEmailBody, url));
            }

            Alerts.AddInfo(Messages.RecoveryInformation);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Reset(String token)
        {
            if (Service.IsLoggedIn(User))
                return RedirectToDefault();

            if (!Validator.CanReset(new AccountResetView { Token = token }))
                return RedirectToAction("Recover");

            return View();
        }

        [HttpPost]
        public ActionResult Reset(AccountResetView account)
        {
            if (Service.IsLoggedIn(User))
                return RedirectToDefault();

            if (!Validator.CanReset(account))
                return RedirectToAction("Recover");

            Service.Reset(account);

            Alerts.AddSuccess(Messages.SuccessfulReset, 4000);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Login(String returnUrl)
        {
            if (Service.IsLoggedIn(User))
                return RedirectToLocal(returnUrl);

            return View();
        }

        [HttpPost]
        public ActionResult Login(AccountLoginView account, String returnUrl)
        {
            if (Service.IsLoggedIn(User))
                return RedirectToLocal(returnUrl);

            if (!Validator.CanLogin(account))
                return View(account);

            Service.Login(HttpContext, account.Username);

            return RedirectToLocal(returnUrl);
        }

        [HttpGet]
        public RedirectToActionResult Logout()
        {
            Service.Logout(HttpContext);

            return RedirectToAction("Login");
        }
    }
}
