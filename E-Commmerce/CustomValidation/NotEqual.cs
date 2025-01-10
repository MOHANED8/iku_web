using E_Commmerce.ViewModels; // For accessing the ProductViewModel
using System.ComponentModel.DataAnnotations; // For ValidationAttribute and ValidationResult

namespace E_Commmerce.CustomValidation
{
    // Custom validation attribute to ensure that the description is not equal to the name
    public class NotEqual : ValidationAttribute
    {
        // Overrides the IsValid method to provide custom validation logic
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            // Cast the validation context object to the ProductViewModel
            ProductViewModel model = (ProductViewModel)validationContext.ObjectInstance;

            // Convert the value being validated (description) to a string
            string Description = value?.ToString();

            // Check if the description is not null
            if (Description != null)
            {
                // If the description is equal to the name, return a validation error
                if (Description == model.Name)
                {
                    return new ValidationResult("Description cannot be equal to name");
                }
            }

            // Return success if validation passes
            return ValidationResult.Success;
        }
    }
}
