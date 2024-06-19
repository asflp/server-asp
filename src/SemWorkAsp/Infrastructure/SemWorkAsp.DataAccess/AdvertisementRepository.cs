using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SemWorkAsp.AppServices.Repositories;
using SemWorkAsp.Contracts;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.Enums;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess;

/// <inheritdoc/>
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly IMapper _mapper;
    private readonly IRepository<Advertisement> _repository;
    public AdvertisementRepository(IRepository<Advertisement> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<ResultWithPagination<AdvertisementShortDto>> GetAllAsync(GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();
        return await WithPagination(query, request, cancellationToken);
    }

    public async Task<ResultWithPagination<AdvertisementShortDto>> GetAllByFilterAsync(GetAllAdvertisementsRequest request, AdvertisementsByFilterRequest filter, CancellationToken cancellationToken)
    {
        var price = GetPrice(filter.PlantPrice);
        var ads = _repository.GetAll()
            .Where(u => u.Price >= price.Item1 && u.Price <= price.Item2);
        var type = GetGroup(filter.PlantType);
        if (type != null)
        {
            ads = ads.Where(a => a.Category == type);
        }

        // if (request.IsRateGood)
        // {
        //     ads = ads.Where(a => a.User.Rate >= 4);
        // }

        return await WithPagination(ads, request, cancellationToken);
    }

    public Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _repository.GetAll().Where(s => s.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ResultWithPagination<AdvertisementShortDto>> GetByNameAsync(string search, GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        var result = _repository.GetFiltered(advertisement =>
            advertisement.Name.Contains(search) || advertisement.Description.Contains(search));
        return await WithPagination(result, request, cancellationToken);
    }

    public async Task<AdvertisementShortDto> AddAsync(Advertisement model, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Advertisement>(model);
        var entityFromDb = await _repository.AddAsync(entity, cancellationToken);
        return _mapper.Map<AdvertisementShortDto>(entityFromDb);
    }

    public Task UpdateAsync(CreateAdvertisementRequest model, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(id, cancellationToken);
    }

    private (int, int) GetPrice(PlantPrice? plantPrice)=> plantPrice switch
    {
        PlantPrice.First => (1, 500),
        PlantPrice.Second => (501, 1000),
        PlantPrice.Third => (1001, 2000),
        PlantPrice.Fourth => (2001, Int32.MaxValue),
        _ => (1, Int32.MaxValue)
    };
    
    private string GetGroup(PlantType? plantType)=> plantType switch
    {
        PlantType.Succulent => "Суккуленты",
        PlantType.Blossoming => "Цветущие",
        PlantType.Leafy => "Лиственные",
        PlantType.Other => "Другие",
        _ => null
    };

    private async Task<ResultWithPagination<AdvertisementShortDto>> WithPagination(IQueryable<Advertisement> advertisements, 
        GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        var result = new ResultWithPagination<AdvertisementShortDto>();

        var elementsCount = await advertisements.CountAsync(cancellationToken);
        if (elementsCount <= request.Batchsize)
        {
            result.AvailablePages = 0;
            result.Result = advertisements.ProjectTo<AdvertisementShortDto>(_mapper.ConfigurationProvider);
            return result;
        }
        
        result.AvailablePages = elementsCount / request.Batchsize;

        var paginationQuery = await advertisements
            .OrderBy(a => a.CreatedAt)
            .Skip(request.Batchsize * (request.PageNumber - 1))
            .Take(request.Batchsize)
            .ProjectTo<AdvertisementShortDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        result.Result = paginationQuery;

        return result;
    }
}