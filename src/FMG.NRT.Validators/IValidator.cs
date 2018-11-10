using Microsoft.AspNetCore.Mvc.ModelBinding;
using FMG.NRT.Components.Alerts;
using System;

namespace FMG.NRT.Validators
{
    public interface IValidator : IDisposable
    {
        ModelStateDictionary ModelState { get; set; }
        Int32 CurrentAccountId { get; set; }
        AlertsContainer Alerts { get; set; }
    }
}
