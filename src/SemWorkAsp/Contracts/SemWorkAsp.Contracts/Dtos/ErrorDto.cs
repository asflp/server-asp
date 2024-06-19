namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Бизнес-модель для ошибок
/// </summary>
public class ErrorDto
{
    /// <summary>
    /// Поле
    /// </summary>
    public string Entry { get; set; }
    
    /// <summary>
    /// Сообщение ошибки
    /// </summary>
    public string ErrorMessage { get; set; }
}