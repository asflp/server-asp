using SemWorkAsp.Contracts.Enums;

namespace SemWorkAsp.Contracts.ModelsRequest;

/// <summary>
/// Модель для запроса отфильтрованных объявлений
/// </summary>
public class AdvertisementsByFilterRequest
{
    /// <summary>
    /// Тип цветка
    /// </summary>
    public PlantType? PlantType { get; set; }
    
    /// <summary>
    /// Цена в объявлении
    /// </summary>
    public PlantPrice? PlantPrice { get; set; }
    
    /// <summary>
    /// Является ли рейтинг продавца от 4 звезд
    /// </summary>
    public bool IsRateGood { get; set; }
}