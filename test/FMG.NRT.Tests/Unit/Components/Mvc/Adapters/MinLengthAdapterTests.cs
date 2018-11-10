using Microsoft.AspNetCore.Mvc;
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
    public class MinLengthAdapterTests
    {
        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_MinLength()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            MinLengthAdapter adapter = new MinLengthAdapter(new MinLengthAttribute(128));
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "StringField");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            String expected = String.Format(Validations.MinLength, context.ModelMetadata.PropertyName, 128);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validations.MinLength, adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
