using AutoMapper;
using Microsoft.AspNetCore.Http;
using SemWorkAsp.Contracts;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

/// <inheritdoc/>
public class AdvertisementService : IAdvertisementService
{
    
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUserService _userService;
    private readonly IMapper _mapper;
    private readonly ILikeRepository _likeRepository;
    
    public AdvertisementService(IAdvertisementRepository advertisementRepository, IUserService userService, IMapper mapper, ILikeRepository likeRepository)
    {
        _advertisementRepository = advertisementRepository;
        _userService = userService;
        _mapper = mapper;
        _likeRepository = likeRepository;
    }

    public async Task<ResultWithPagination<AdvertisementShortDto>> GetAdvertisementsAsync(GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        return await _advertisementRepository.GetAllAsync(request, cancellationToken);
    }

    public async Task<ResultWithPagination<AdvertisementShortDto>> GetAdvertisementsByNameAsync(string? search, GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(search))
        {
            return await GetAdvertisementsAsync(request, cancellationToken);
        }

        return await _advertisementRepository.GetByNameAsync(search, request, cancellationToken);
    }

    public async Task<ResultWithPagination<AdvertisementShortDto>> GetByFilterAsync(AdvertisementsByFilterRequest filter, GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        if (filter.PlantPrice == null && filter.PlantType == null && !filter.IsRateGood)
        {
            return await GetAdvertisementsAsync(request, cancellationToken);
        }

        return await _advertisementRepository.GetAllByFilterAsync(request, filter, cancellationToken);
    }

    public async Task<AdvertisementWithUser> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var ad = await _advertisementRepository.GetByIdAsync(id, cancellationToken);
        return new AdvertisementWithUser
        {
            Building = ad.Building,
            Category = ad.Category,
            City = ad.City,
            Description = ad.Description,
            Likes = ad.Likes,
            Name = ad.Name,
            PhotoUrls = ad.PhotoUrls,
            Price = ad.Price,
            Street = ad.Street,
            User = await _userService.GetUserByIdAsync(ad.UserId.ToString(), cancellationToken),
            UserId = ad.UserId
        };
    }

    public async Task<AdvertisementShortDto> AddAsync(CreateAdvertisementRequest model, string userId, CancellationToken cancellationToken)
    {
        var images = new List<string>();
        
        if (model.Images != null)
        {

            foreach (ImageDto image in model.Images)
            {
                var responseImgBb =  await HttpImgBbClient.UploadImage(ConvertIFormFileToByteArray(image.Image));
                if (responseImgBb != null && responseImgBb.Status != 400) images.Add(responseImgBb.Data.URL);
            }
        }

        var advertisement = _mapper.Map<Advertisement>(model);
        advertisement.UserId = Guid.Parse(userId);
        advertisement.PhotoUrls = images;
        
        return await _advertisementRepository.AddAsync(advertisement, cancellationToken);
    }

    public async Task UpdateAsync(CreateAdvertisementRequest model, CancellationToken cancellationToken)
    {
        await _advertisementRepository.UpdateAsync(model, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        await _advertisementRepository.DeleteAsync(id, cancellationToken);
    }

    public async Task<bool> LikeAsync(string userId, Guid advertisementId, CancellationToken cancellationToken)
    {
        try
        {
            var like = new Like
            {
                UserId = Guid.Parse(userId),
                AdvertisementId = advertisementId
            };
            await _likeRepository.AddLikeAsync(like, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public async Task<bool> UnlikeAsync(string userId, Guid advertisementId, CancellationToken cancellationToken)
    {
        try
        {
            var like = new Like
            {
                UserId = Guid.Parse(userId),
                AdvertisementId = advertisementId
            };
            await _likeRepository.DeleteLikeAsync(like, cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
    
    private byte[] ConvertIFormFileToByteArray(IFormFile file)
    {
        using var memoryStream = new MemoryStream();
        file.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}