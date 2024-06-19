using SemWorkAsp.Contracts;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

/// <summary>
/// Сервис для работы с объявлениями
/// </summary>
public interface IAdvertisementService
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetAdvertisementsAsync(GetAllAdvertisementsRequest request,
        CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetAdvertisementsByNameAsync(string search, 
        GetAllAdvertisementsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetByFilterAsync(AdvertisementsByFilterRequest filter,
        GetAllAdvertisementsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<AdvertisementWithUser> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<AdvertisementShortDto> AddAsync(CreateAdvertisementRequest model, string id, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task UpdateAsync(CreateAdvertisementRequest model, CancellationToken cancellationToken);
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> LikeAsync(string userId, Guid advertisementId, CancellationToken cancellationToken);
    
    Task<bool> UnlikeAsync(string userId, Guid advertisementId, CancellationToken cancellationToken);
}
