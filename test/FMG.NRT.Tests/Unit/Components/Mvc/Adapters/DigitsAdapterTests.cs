﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using FMG.NRT.Components.Mvc;
using FMG.NRT.Resources.Form;
using FMG.NRT.Tests.Objects;
using System;
using System.Collections.Generic;
using Xunit;

namespace FMG.NRT.Tests.Unit.Components.Mvc
{
    public class DigitsAdapterTests
    {
        private DigitsAdapter adapter;
        private ClientModelValidationContext context;
        private Dictionary<String, String> attributes;

        public DigitsAdapterTests()
        {
            attributes = new Dictionary<String, String>();
            adapter = new DigitsAdapter(new DigitsAttribute());
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            context = new ClientModelValidationContext(new ActionContext(), metadata, provider, attributes);
        }

        #region AddValidation(ClientModelValidationContext context)

        [Fact]
        public void AddValidation_Digits()
        {
            adapter.AddValidation(context);

            Assert.Equal(2, attributes.Count);
            Assert.Equal("true", attributes["data-val"]);
            Assert.Equal(String.Format(Validations.Digits, context.ModelMetadata.PropertyName), attributes["data-val-digits"]);
        }

        #endregion

        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_Digits()
        {
            String expected = String.Format(Validations.Digits, context.ModelMetadata.PropertyName);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
