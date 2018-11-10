﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using FMG.NRT.Components.Mvc;
using FMG.NRT.Objects;
using FMG.NRT.Resources;
using System;
using Xunit;

namespace FMG.NRT.Tests.Unit.Components.Mvc
{
    public class DisplayMetadataProviderTests
    {
        #region CreateDisplayMetadata(DisplayMetadataProviderContext context)

        [Fact]
        public void CreateDisplayMetadata_SetsDisplayName()
        {
            DisplayMetadataProvider provider = new DisplayMetadataProvider();
            DisplayMetadataProviderContext context = new DisplayMetadataProviderContext(
                ModelMetadataIdentity.ForProperty(typeof(String), "Title", typeof(RoleView)),
                ModelAttributes.GetAttributesForType(typeof(RoleView)));

            provider.CreateDisplayMetadata(context);

            String expected = ResourceProvider.GetPropertyTitle(typeof(RoleView), "Title");
            String actual = context.DisplayMetadata.DisplayName();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CreateDisplayMetadata_NullContainerType_DoesNotSetDisplayName()
        {
            DisplayMetadataProvider provider = new DisplayMetadataProvider();
            DisplayMetadataProviderContext context = new DisplayMetadataProviderContext(
                   ModelMetadataIdentity.ForType(typeof(RoleView)),
                   ModelAttributes.GetAttributesForType(typeof(RoleView)));

            provider.CreateDisplayMetadata(context);

            Assert.Null(context.DisplayMetadata.DisplayName);
        }

        #endregion
    }
}
