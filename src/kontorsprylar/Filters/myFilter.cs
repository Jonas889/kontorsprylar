using Microsoft.AspNet.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace Övning3.Filters
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class MustBeTrueAttribute : ValidationAttribute, IClientModelValidator
    {
       

        public override bool IsValid(object value)
        {
            return value != null && value is bool && (bool)value;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ClientModelValidationContext context)
        {
            yield return new ModelClientValidationRule("mustbetrue", ErrorMessage = ErrorMessageString);
        }
    }
}

