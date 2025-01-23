using AuthApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AuthApp.Attributes
{
    public class PasswordValidationAttribute: ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var user = (UserCreateViewModel)validationContext.ObjectInstance;
            if (user.Password != user.ConfirmPassword)
                return new ValidationResult("Passwords do not match");

            return ValidationResult.Success;
        }
    }
}
