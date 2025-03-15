using Data.LohikaBackend.Entities.Identity;
using FluentValidation;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace LohikaBackend.Validators
{
    public class ValidatorProductAddViewModel : AbstractValidator<ProductAddViewModel>
    {
        public ValidatorProductAddViewModel()
        {
            RuleFor(x => x.Name)
               .NotEmpty().WithMessage("Назва є обов'язковим!");

            RuleFor(x => x.Priority)
                .GreaterThanOrEqualTo(1)
                .WithMessage("Мінімальне начення 1");

            
        }
    }
}
