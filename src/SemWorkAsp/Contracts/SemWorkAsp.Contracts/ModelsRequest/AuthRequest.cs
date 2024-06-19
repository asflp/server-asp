namespace SemWorkAsp.Contracts.ModelsRequest;

/// <summary>
/// Модель запроса на вход
/// </summary>
public class AuthRequest
{
    /// <summary>
    /// Адрес электронной почты
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
}