using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SemWorkAsp.AppServices.Repositories;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;

namespace SemWorkAsp.Api.Controllers;

[ApiController]
[Route("[controller]")]
[ProducesResponseType((int)HttpStatusCode.InternalServerError)]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IValidator<CreateUserRequest> _validator;
    private readonly IConfiguration _configuration;
    private readonly ILogger<UserController> _logger;

    public UserController(IUserService userService, IValidator<CreateUserRequest> validator, IConfiguration configuration, ILogger<UserController> logger)
    {
        _userService = userService;
        _validator = validator;
        _configuration = configuration;
        _logger = logger;
    }

    /// <summary>
    /// Получение всех пользователей системы
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Все пользователи</returns>
    [HttpGet]
    [Route("all")]
    [ProducesResponseType(typeof(IEnumerable<UserDto>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        _logger.LogError("Запрос на получение всех пользователей");
        var result = await _userService.GetAllAsync(cancellationToken);
        return Ok(result);
    }  
    
    /// <summary>
    /// Создание нового пользователя
    /// </summary>
    /// <param name="request">Пользователь <see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Созданный пользователь</returns>
    [HttpPost]
    [Route("new")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(List<ErrorDto>), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> RegisterNewUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос на регистрацию");
        var validateAsync = await _validator.ValidateAsync(request, cancellationToken);
        if (!validateAsync.IsValid)
        {
            return BadRequest(GetErrors(validateAsync));
        }
        var result = await _userService.RegisterUserAsync(request, cancellationToken);
        return Created(new Uri($"{Request.Path}/{result.Id}", UriKind.Relative), result);
    }

    /// <summary>
    /// Авторизация пользователя
    /// </summary>
    /// <param name="request">Модель запроса <see cref="AuthRequest"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns></returns>
    [HttpPost]
    [Route("auth")]
    [ProducesResponseType(typeof(UserDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(List<ErrorDto>), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
    public async Task<IActionResult> EnterAccountAsync(AuthRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Запрос на вход в аккаунт");
            if (!await _userService.CheckUserByEmailAsync(request, cancellationToken))
            {
                return BadRequest(new ErrorDto
                {
                    Entry = "Email",
                    ErrorMessage = "Такой email еще не зарегестрирован"
                });
            }

            var user = await _userService.GetUserByEmail(request, cancellationToken);
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["JwtAuth:Key"]!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience =  _configuration["JwtAuth:Audience"],
                Issuer = _configuration["JwtAuth:Issuer"],
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                
                Expires = DateTime.UtcNow.AddMinutes(40),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            user.Token = new JwtSecurityTokenHandler().WriteToken(token);
            Console.WriteLine(user);
            return Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError("Ошибка при входе в аккаунт");
            return Unauthorized(ex.Message);
        }
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