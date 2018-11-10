﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using FMG.NRT.Components.Security;
using System;
using System.Diagnostics.CodeAnalysis;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    [Authorize]
    [ExcludeFromCodeCoverage]
    public class AuthorizedController : Controller
    {
        [HttpGet]
        public ViewResult Action()
        {
            return null;
        }

        [HttpPost]
        public ViewResult Action(Object obj)
        {
            return null;
        }

        [HttpGet]
        [AllowAnonymous]
        public ViewResult AllowAnonymousAction()
        {
            return null;
        }

        [HttpGet]
        [AllowUnauthorized]
        public ViewResult AllowUnauthorizedAction()
        {
            return null;
        }
    }
}
