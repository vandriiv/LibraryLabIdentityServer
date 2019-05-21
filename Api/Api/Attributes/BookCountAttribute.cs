using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryLab.Attributes
{
    public class BookCountAttribute : ValidationAttribute
    {
        private readonly string availableCount;
        public BookCountAttribute(string other)
        {
            availableCount = other;
        }

        protected override ValidationResult IsValid(object totalCount, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(availableCount);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", availableCount)
                );
            }
            var availableCountValue = property.GetValue(validationContext.ObjectInstance, null);


            if ((int)totalCount < (int)availableCountValue)
            {

                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            return null;
        }
    }
}

