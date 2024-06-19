namespace SemWorkAsp.Domain.Entities;

/// <summary>
/// Сущность многие ко многим для понравившихся объявлений
/// </summary>
public class Like
{
    public Guid UserId { get; set; }
    public User User { get; set; }
    public Guid AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; }
}