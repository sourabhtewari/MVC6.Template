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
    public class RangeAdapterTests
    {
        #region GetErrorMessage(ModelValidationContextBase context)

        [Fact]
        public void GetErrorMessage_Range()
        {
            IModelMetadataProvider provider = new EmptyModelMetadataProvider();
            RangeAdapter adapter = new RangeAdapter(new RangeAttribute(4, 128));
            ModelMetadata metadata = provider.GetMetadataForProperty(typeof(AllTypesView), "Int32Field");
            ModelValidationContextBase context = new ModelValidationContextBase(new ActionContext(), metadata, provider);

            String expected = String.Format(Validations.Range, context.ModelMetadata.PropertyName, 4, 128);
            String actual = adapter.GetErrorMessage(context);

            Assert.Equal(Validations.Range, adapter.Attribute.ErrorMessage);
            Assert.Equal(expected, actual);
        }

        #endregion
    }
}
