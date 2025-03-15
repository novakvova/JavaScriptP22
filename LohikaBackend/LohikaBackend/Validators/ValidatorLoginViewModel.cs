using Data.LohikaBackend.Entities.Identity;
using FluentValidation;
using LohikaBackend.Models;
using Microsoft.AspNetCore.Identity;

namespace LohikaBackend.Validators
{
    public class ValidatorLoginViewModel : AbstractValidator<LoginViewModel>
    {
        public ValidatorLoginViewModel()
        {
            RuleFor(x => x.Email)
               .NotEmpty().WithMessage("Поле пошта є обов'язковим!")
               .EmailAddress().WithMessage("Пошта є не коректною!");
               
            RuleFor(x => x.Password)
                .NotEmpty().WithName("Password").WithMessage("Поле пароль є обов'язковим!")
                .MinimumLength(5).WithName("Password").WithMessage("Поле пароль має містити міннімум 5 символів!");
        }
    }
}
