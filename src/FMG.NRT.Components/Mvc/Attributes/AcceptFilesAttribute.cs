using Microsoft.AspNetCore.Http;
using FMG.NRT.Resources.Form;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FMG.NRT.Components.Mvc
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public class AcceptFilesAttribute : ValidationAttribute
    {
        public String Extensions { get; }

        public AcceptFilesAttribute(String extensions)
            : base(() => Validations.AcceptFiles)
        {
            Extensions = extensions;
        }

        public override String FormatErrorMessage(String name)
        {
            return String.Format(ErrorMessageString, name, Extensions);
        }
        public override Boolean IsValid(Object value)
        {
            IEnumerable<IFormFile> files = ToFiles(value);

            if (value == null)
                return true;

            if (files == null)
                return false;

            return files.All(file => Extensions.Split(',').Any(extension => file.FileName?.EndsWith(extension) == true));
        }

        private IEnumerable<IFormFile> ToFiles(Object value)
        {
            return value is IFormFile file ? new[] { file } : value as IEnumerable<IFormFile>;
        }
    }
}
