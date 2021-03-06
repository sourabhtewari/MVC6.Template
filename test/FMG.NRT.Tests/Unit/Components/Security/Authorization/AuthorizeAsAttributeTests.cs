﻿using FMG.NRT.Components.Security;
using System;
using Xunit;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    public class AuthorizeAsAttributeTests
    {
        #region AuthorizeAsAttribute(String action)

        [Fact]
        public void AuthorizeAsAttribute_NullAction_Throws()
        {
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(() => new AuthorizeAsAttribute(null));
            Assert.Equal("action", exception.ParamName);
        }

        [Fact]
        public void AuthorizeAsAttribute_SetsAction()
        {
            String actual = new AuthorizeAsAttribute("Action").Action;
            String expected = "Action";

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
