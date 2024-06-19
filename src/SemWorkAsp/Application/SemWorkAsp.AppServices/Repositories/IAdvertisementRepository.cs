using SemWorkAsp.Contracts;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

/// <summary>
/// Репозиторий для объявлений
/// </summary>
public interface IAdvertisementRepository
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetAllAsync(GetAllAdvertisementsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetAllByFilterAsync(GetAllAdvertisementsRequest request, AdvertisementsByFilterRequest filter, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="search"></param>
    /// <param name="filter"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<ResultWithPagination<AdvertisementShortDto>> GetByNameAsync(string search, GetAllAdvertisementsRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<AdvertisementShortDto> AddAsync(Advertisement model, CancellationToken cancellationToken);
    
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
}