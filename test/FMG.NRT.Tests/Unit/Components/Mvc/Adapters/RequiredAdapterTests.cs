﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Components.Mvc;
using FMG.NRT.Resources.Form;
using FMG.NRT.Tests.Objects;
using System;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace FMG.NRT.Tests.Unit.Components.Mvc
{
    public class RequiredAdapterTests
    {
        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_Required()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            RequiredAdapter adapter = new RequiredAdapter(new RequiredAttribute());
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            String expected = String.Format(Validations.Required, context.ModelMetadata.PropertyName);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validations.Required, adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
