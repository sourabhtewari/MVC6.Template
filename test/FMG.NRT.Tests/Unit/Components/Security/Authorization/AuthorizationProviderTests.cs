﻿using FMG.NRT.Components.Security;
using FMG.NRT.Data.Core;
using FMG.NRT.Objects;
using FMG.NRT.Tests.Data;
using NSubstitute;
using System;
using System.Reflection;
using Xunit;

namespace FMG.NRT.Tests.Unit.Components.Security
{
    public class AuthorizationProviderTests
    {
        private TestingContext context;
        private AuthorizationProvider authorization;

        public AuthorizationProviderTests()
        {
            context = new TestingContext();
            IServiceProvider services = Substitute.For<IServiceProvider>();
            services.GetService(typeof(IUnitOfWork)).Returns(info => new UnitOfWork(new TestingContext(context.DatabaseName)));

            authorization = new AuthorizationProvider(Assembly.GetExecutingAssembly(), services);
        }

        #region IsAuthorizedFor(Int32? accountId, String area, String controller, String action)

        [Fact]
        public void IsAuthorizedFor_AuthorizesControllerByIgnoringCase()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "AUTHORIZED", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeControllerByIgnoringCase()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "AUTHORIZED", "Action"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsAuthorizedFor_AuthorizesControllerWithoutArea(String area)
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, area, "Authorized", "Action"));
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void IsAuthorizedFor_DoesNotAuthorizeControllerWithoutArea(String area)
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, area, "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesControllerWithArea()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeControllerWithArea()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedGetAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedGetAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedGetAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesNamedGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedNamedGetAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedNamedGetAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeNamedGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedNamedGetAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesNotExistingAction()
        {
            Assert.True(authorization.IsAuthorizedFor(null, null, "Authorized", "Test"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesNonGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedPostAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedPostAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeNonGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedPostAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesNamedNonGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedNamedPostAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedNamedPostAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeNamedNonGetAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedNamedPostAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesActionAsAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeActionAsAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Authorized", "Action");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesActionAsSelf()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedAsSelf");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsSelf"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeActionAsSelf()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsSelf"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesActionAsOtherAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "InheritedAuthorized", "InheritanceAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsOtherAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeActionAsOtherAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "AuthorizedAsOtherAction");

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "AuthorizedAsOtherAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesEmptyAreaAsNull()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, "", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeEmptyAreaAsNull()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAuthorizedAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "AllowAnonymous", "AuthorizedAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "AllowAnonymous", "AuthorizedAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeAuthorizedAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, null, "AllowAnonymous", "AuthorizedAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAllowAnonymousAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "Authorized", "AllowAnonymousAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAllowUnauthorizedAction()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "Authorized", "AllowUnauthorizedAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAuthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeAuthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, null, "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAllowAnonymousController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "AllowAnonymous", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesAllowUnauthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "AllowUnauthorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesInheritedAuthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "InheritedAuthorized", "InheritanceAction");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "InheritedAuthorized", "InheritanceAction"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeInheritedAuthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, null, "InheritedAuthorized", "InheritanceAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesInheritedAllowAnonymousController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "InheritedAllowAnonymous", "InheritanceAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesInheritedAllowUnauthorizedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "InheritedAllowUnauthorized", "InheritanceAction"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesNotAttributedController()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Test", "Test");

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "NotAttributed", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeNotExistingAccount()
        {
            CreateAccountWithPermissionFor("Area", "Authorized", "Action");

            Assert.False(authorization.IsAuthorizedFor(0, "Area", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeLockedAccount()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action", isLocked: true);

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeNullAccount()
        {
            CreateAccountWithPermissionFor(null, "Authorized", "Action");

            Assert.False(authorization.IsAuthorizedFor(null, null, "Authorized", "Action"));
        }

        [Fact]
        public void IsAuthorizedFor_AuthorizesByIgnoringCase()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action");

            Assert.True(authorization.IsAuthorizedFor(accountId, "area", "authorized", "action"));
        }

        [Fact]
        public void IsAuthorizedFor_DoesNotAuthorizeByIgnoringCase()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Test", "Test", "Test");

            Assert.False(authorization.IsAuthorizedFor(accountId, "area", "authorized", "action"));
        }

        [Fact]
        public void IsAuthorizedFor_CachesAccountPermissions()
        {
            Int32 accountId = CreateAccountWithPermissionFor(null, "Authorized", "Action");

            context.Database.EnsureDeleted();

            Assert.True(authorization.IsAuthorizedFor(accountId, null, "Authorized", "Action"));
        }

        #endregion

        #region Refresh()

        [Fact]
        public void Refresh_Permissions()
        {
            Int32 accountId = CreateAccountWithPermissionFor("Area", "Authorized", "Action");
            Assert.True(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "Action"));

            context.Database.EnsureDeleted();

            authorization.Refresh();

            Assert.False(authorization.IsAuthorizedFor(accountId, "Area", "Authorized", "Action"));
        }

        #endregion

        #region Test helpers

        private Int32 CreateAccountWithPermissionFor(String area, String controller, String action, Boolean isLocked = false)
        {
            using (TestingContext testingContext = new TestingContext(context.DatabaseName))
            {
                RolePermission rolePermission = ObjectFactory.CreateRolePermission();
                Account account = ObjectFactory.CreateAccount();
                account.Role.Permissions.Add(rolePermission);
                rolePermission.Role = account.Role;
                account.IsLocked = isLocked;

                rolePermission.Permission.Controller = controller;
                rolePermission.Permission.Action = action;
                rolePermission.Permission.Area = area;

                testingContext.Add(account);
                testingContext.SaveChanges();

                authorization.Refresh();

                return account.Id;
            }
        }

        #endregion
    }
}
