using SemWorkAsp.Domain.Base;

namespace SemWorkAsp.Domain.Entities;

/// <summary>
/// Сущность пользователя
/// </summary>
public class User : BaseEntity
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
    /// Роль
    /// </summary>
    public string Role { get; set; }
    
    /// <summary>
    /// Адрес почты
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Ссылка на аватар профиля
    /// </summary>
    public string? AvatarUrl { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Описание профиля
    /// </summary>
    public string? Description { get; set; }
    
    /// <summary>
    /// Объявления пользователя
    /// </summary>
    public ICollection<Advertisement>? Advertisements{ get; set; }
    
    /// <summary>
    /// Понравившиеся объявления
    /// </summary>
    public ICollection<Like>? Likes { get; set; }
    
    public ICollection<Advertisement>? Cart { get; set; }
}