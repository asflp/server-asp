using FluentValidation;
using FluentValidation.Results;
using SemWorkAsp.Contracts.Dtos;

namespace SemWorkAsp.AppServices.Validation;

/// <summary>
/// Валидатор для создания пользователя
/// </summary>
public class CreateUserValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserValidator()
    {
        RuleFor(u => u.Name)
            .NotEmpty()
            .WithMessage("Имя не может быть пустым")
            .MaximumLength(50)
            .WithMessage("Максимальная длина - 50 символов")
            .MinimumLength(2)
            .WithMessage("Минимальная длина - 2 символов")
            .Must(s => s.All(Char.IsLetter))
            .WithMessage("Все символы должны быть буквами");
        
        RuleFor(u => u.Surname)
            .NotEmpty()
            .WithMessage("Фамилия не может быть пустой")
            .MaximumLength(50)
            .WithMessage("Максимальная длина - 50 символов")
            .MinimumLength(2)
            .WithMessage("Минимальная длина - 2 символов")
            .Must(s => s.All(Char.IsLetter))
            .WithMessage("Все символы должны быть буквами");

        RuleFor(u => u.Email)
            .NotEmpty()
            .WithMessage("Почта не может быть пустой")
            .EmailAddress()
            .WithMessage("Это не адрес электронной почты");
        
        RuleFor(u => u.PhoneNumber)
            .NotEmpty()
            .WithMessage("Номер телефона не может быть пустым")
            .MaximumLength(20)
            .WithMessage("Максимальная длина - 50 символов")
            .MinimumLength(2)
            .WithMessage("Минимальная длина - 2 символов")
            .Must(s => s.All(Char.IsDigit))
            .WithMessage("Все символы должны быть цифрами");
        
        RuleFor(u => u.Password)
            .NotEmpty()
            .WithMessage("Пароль не может быть пустым")
            .MaximumLength(50)
            .WithMessage("Максимальная длина - 50 символов")
            .MinimumLength(8)
            .WithMessage("Минимальная длина - 8 символов")
            .Must(s => s.Any(Char.IsLetter))
            .WithMessage("Пароль должен содержать хотя бы 1 букву")
            .Must(s => s.Any(Char.IsDigit))
            .WithMessage("Пароль должен содержать хотя бы 1 цифру");
    }
}