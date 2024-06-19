using System.Net;
using System.Security.Claims;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SemWorkAsp.AppServices.Repositories;
using SemWorkAsp.Contracts;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class AdvertisementController : Controller
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IValidator<CreateAdvertisementRequest> _validator;
    private readonly ILogger<AdvertisementController> _logger;
    
    public AdvertisementController(IAdvertisementService advertisementService, IValidator<CreateAdvertisementRequest> validator, ILogger<AdvertisementController> logger)
    {
        _advertisementService = advertisementService;
        _validator = validator;
        _logger = logger;
    }

    [HttpGet]
    [Route("all")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(IEnumerable<AdvertisementShortDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllAdvertisementsAsync([FromQuery] GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("запрос на пооучение всех объявлений");
        var result = await _advertisementService.GetAdvertisementsAsync(request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("search")]
    [ProducesResponseType(typeof(IEnumerable<AdvertisementShortDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAdvertisementsBySearchAsync([FromQuery] string search, [FromQuery] GetAllAdvertisementsRequest request, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.GetAdvertisementsByNameAsync(search, request, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet]
    [Route("{id:Guid}")]
    [ProducesResponseType(typeof(Advertisement), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAdvertisementByIdAsync(string id, CancellationToken cancellationToken)
    {
        try
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier);
            var result = await _advertisementService.GetByIdAsync(Guid.Parse(id), cancellationToken);
            result.IsLike = userId != null && result.Likes.Any(l => l.UserId.ToString() == userId.Value);
            return Ok(result);
        }
        catch (Exception e)
        {
            return NotFound(e);
        }
    }
    
    [HttpGet]
    [Route("filtred")]
    [ProducesResponseType(typeof(IEnumerable<AdvertisementShortDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAdvertisementsByFilterAsync([FromQuery] GetAllAdvertisementsRequest request, [FromQuery] AdvertisementsByFilterRequest filter, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.GetByFilterAsync(filter, request, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpPost]
    [Route("new")]
    [ProducesResponseType(typeof(AdvertisementShortDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(List<ErrorDto>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> CreateAdvertisementAsync(CreateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var validateAsync = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateAsync.IsValid)
        {
            return BadRequest(GetErrors(validateAsync));
        }
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _advertisementService.AddAsync(request, userId, cancellationToken);
        return Created(new Uri($"{Request.Path}/{result.Id}", UriKind.Relative), result);
    }
    
    [Authorize]
    [HttpPut]
    [Route("update/{id:Guid}")]
    [ProducesResponseType(typeof(AdvertisementShortDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDto>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UpdateAdvertisementAsync(CreateAdvertisementRequest request, CancellationToken cancellationToken)
    {
        var validateAsync = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateAsync.IsValid)
        {
            return BadRequest(GetErrors(validateAsync));
        }
        
        await _advertisementService.UpdateAsync(request, cancellationToken);
        return await Task.Run(() => Ok(new UserDto()), cancellationToken);
    }

    [Authorize]
    [HttpPost]
    [Route("like/{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> LikeAdvertisement(Guid advertisementId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _advertisementService.LikeAsync(userId, advertisementId, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("unlike/{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> UnlikeAdvertisement(Guid advertisementId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _advertisementService.UnlikeAsync(userId, advertisementId, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpGet]
    [Route("likes/{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetFavorite(Guid advertisementId, CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)!.Value;
        var result = await _advertisementService.UnlikeAsync(userId, advertisementId, cancellationToken);
        return Ok(result);
    }
    
    [Authorize]
    [HttpDelete]
    [Route("delete/{id:Guid}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> DeleteAdvertisementAsync(Guid id, CancellationToken cancellationToken)
    {
        await _advertisementService.DeleteAsync(id, cancellationToken);
        return Ok();
    }
    
    private static List<ErrorDto> GetErrors(ValidationResult validationResult)
    {
        var list = new List<ErrorDto>();
            
        foreach (var failure in validationResult.Errors)
        {
            list.Add(new ErrorDto
            {
                Entry = failure.PropertyName,
                ErrorMessage = failure.ErrorMessage
            });
        }
    
        return list;
    }
}