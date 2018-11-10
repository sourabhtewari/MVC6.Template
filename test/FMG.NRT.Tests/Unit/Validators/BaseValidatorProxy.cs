using FMG.NRT.Data.Core;
using FMG.NRT.Objects;
using FMG.NRT.Validators;
using System;
using System.Linq.Expressions;

namespace FMG.NRT.Tests.Unit.Validators
{
    public class BaseValidatorProxy : BaseValidator
    {
        public BaseValidatorProxy(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Boolean BaseIsSpecified<TView>(TView view, Expression<Func<TView, Object>> property) where TView : BaseView
        {
            return IsSpecified(view, property);
        }
    }
}
