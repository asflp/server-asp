using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

public interface ILikeRepository
{
    Task AddLikeAsync(Like like, CancellationToken cancellationToken);
    
    Task DeleteLikeAsync(Like like, CancellationToken cancellationToken);
}