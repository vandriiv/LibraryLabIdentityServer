using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.Extensions
{
    public static class ModelStateHelper
    {
        public static IEnumerable<string> Errors(this ModelStateDictionary modelState)
        {
            if (!modelState.IsValid)
            {
                var errors = modelState.Values.SelectMany(m => m.Errors)
                                 .Select(e => e.ErrorMessage).ToList();
                return errors;
                                 
            }
            return null;
        }
    }
}
