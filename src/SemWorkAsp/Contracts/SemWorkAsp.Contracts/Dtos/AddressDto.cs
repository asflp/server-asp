namespace SemWorkAsp.Contracts.Dtos;

/// <summary>
/// Объект бизнес-логики адреса
/// </summary>
public class AddressDto
{
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