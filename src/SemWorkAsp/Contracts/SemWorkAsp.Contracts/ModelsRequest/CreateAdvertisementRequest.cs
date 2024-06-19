using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Модель создания объявления
/// </summary>
public class CreateAdvertisementRequest
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
    /// Стоимость товара
    /// </summary>
    public int Price { get; set; }
    
    /// <summary>
    /// Описание
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// Фото объявления
    /// </summary>
    public List<ImageDto>? Images { get; set; }
    
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
}