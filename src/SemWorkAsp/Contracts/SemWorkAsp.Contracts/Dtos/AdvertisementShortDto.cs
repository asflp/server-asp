using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Объект бизнес-логики объявления для превью
/// </summary>
public class AdvertisementShortDto
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Название
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Username { get; set; }
    
    /// <summary>
    /// Цена
    /// </summary>
    public int Price { get; set; }
    
    /// <summary>
    /// Ссылка
    /// </summary>
    public string PhotoUrl { get; set; }
    
    /// <summary>
    /// Наличие лайка
    /// </summary>
    public bool IsLike { get; set; }
}