namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Модель создания пользователя
/// </summary>
public class CreateUserRequest
{
    /// <summary>
    /// Имя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Фамилия
    /// </summary>
    public string Surname { get; set; }
    
    /// <summary>
    /// Адрес почты
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; }
        
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}