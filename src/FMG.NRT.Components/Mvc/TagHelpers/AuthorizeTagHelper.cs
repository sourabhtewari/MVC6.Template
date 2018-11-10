﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using FMG.NRT.Components.Extensions;
using FMG.NRT.Components.Security;
using System;

namespace FMG.NRT.Components.Mvc
{
    [HtmlTargetElement("authorize", Attributes = "area")]
    [HtmlTargetElement("authorize", Attributes = "action")]
    [HtmlTargetElement("authorize", Attributes = "controller")]
    public class AuthorizeTagHelper : TagHelper
    {
        public String Area { get; set; }
        public String Action { get; set; }
        public String Controller { get; set; }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }

        private IAuthorizationProvider Authorization { get; }

        public AuthorizeTagHelper(IAuthorizationProvider authorization)
        {
            Authorization = authorization;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = null;

            Int32? accountId = ViewContext.HttpContext.User.Id();
            String area = Area ?? ViewContext.RouteData.Values["area"] as String;
            String action = Action ?? ViewContext.RouteData.Values["action"] as String;
            String controller = Controller ?? ViewContext.RouteData.Values["controller"] as String;

            if (Authorization?.IsAuthorizedFor(accountId, area, controller, action) == false)
                output.SuppressOutput();
        }
    }
}
