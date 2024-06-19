using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

/// <summary>
/// Сущность объявления
/// </summary>
public class AdvertisementWithUser
{
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Категория
    /// </summary>
    public string Category { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Ссылки на фото
    /// </summary>
    public List<string>? PhotoUrls { get; set; }
    
    /// <summary>
    /// Город
    /// </summary>
    public string City { get; set; }
    
    /// <summary>
    /// Улица
    /// </summary>
    public string Street { get; set; }
    
    /// <summary>
    /// Строение
    /// </summary>
    public string Building { get; set; }
    
    public Guid UserId { get; set; }
    
    /// <summary>
    /// Пользователь, чье объявление
    /// </summary>
    public UserDto User { get; set; }
    
    public bool IsLike { get; set; }
    
    public ICollection<Like> Likes { get; set; }
}
