using FluentValidation;
using FluentValidation.Results;
using SemWorkAsp.Contracts.Dtos;

namespace SemWorkAsp.AppServices.Validation;

/// <summary>
/// Валидатор при создании нового объявления
/// </summary>
public class CreateAdvertisementValidator : AbstractValidator<CreateAdvertisementRequest>
{
    private readonly List<string> _advertisementCategories = new() { "Лиственные", "Суккуленты", "Цветущие", "Другие"};
    
    public CreateAdvertisementValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty()
            .WithMessage("Название не может быть пустым")
            .MinimumLength(2)
            .WithMessage("Название должно содержать минимум 2 символа")
            .MaximumLength(50)
            .WithMessage("Название может быть максимум 50 символов");

        RuleFor(a => a.Category)
            .NotEmpty()
            .WithMessage("Категория не может быть пустой")
            .Must(a => _advertisementCategories.Contains(a))
            .WithMessage("Такой категории не существует");

        RuleFor(a => a.Price)
            .NotEmpty()
            .WithMessage("Цена не может быть пустой")
            .InclusiveBetween(1, 1000000)
            .WithMessage("Цена может быть от 1 до 1000000");

        RuleFor(a => a.Description)
            .MaximumLength(1000)
            .WithMessage("Описание может быть максимум 1000 символов");

        RuleFor(a => a.Images)
            .Must(x => x.Count is >= 0 and <= 10)
            .WithMessage("Количество изображений может быть от 0 д 10")
            .Must(x => x.All(image => image.FileName != ""))
            .WithMessage("Имя файла не может быть пустым")
            .Must(x => x.All(image => image.Image.Length > 0 && image.Image.ContentType.Contains("image")))
            .WithMessage("Изображения не могут быть пустыми");
        
        RuleFor(x => x.City)
            .NotEmpty()
            .WithMessage("Название города не может быть пустым");
    }
    
    public Dictionary<string, string>? GetErrors(ValidationResult validationResult)
    {
        if (!validationResult.IsValid)
        {
            var dict = new Dictionary<string, string>();
            
            foreach (var failure in validationResult.Errors)
            {
                dict.TryAdd(failure.PropertyName, failure.ErrorMessage);
            }

            return dict;
        }

        return null;
    }
}