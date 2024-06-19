using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Объект бизнес-логики пользователя
/// </summary>
public class UserDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Пароль
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Ссылка на аватар профиля
    /// </summary>
    public string AvatarUrl { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string PhoneNumber { get; set; }
    
    /// <summary>
    /// Описание профиля
    /// </summary>
    public string Description { get; set; }
    
    // /// <summary>
    // /// Объявления пользователя
    // /// </summary>
    // public List<Advertisement> Advertisements { get; set; }
    
    /// <summary>
    /// Фамилия и имя
    /// </summary>
    public string FullName { get; set; }
    
    /// <summary>
    /// Количество объявлений
    /// </summary>
    public int AmountAdvertisments { get; set; }
    
    public string Token { get; set; }
    
    public string Role { get; set; }
    //
    // /// <summary>
    // /// Понравившиеся объявления
    // /// </summary>
    // public List<Advertisement> LikeAdvertisements { get; set; }
}