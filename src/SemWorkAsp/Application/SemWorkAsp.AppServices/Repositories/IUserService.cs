using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;

namespace SemWorkAsp.AppServices.Repositories;

/// <summary>
/// Сервис по управлению пользователями
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Коллекция <see cref="UserDto"/></returns>
    public Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Регистрация нового пользователя
    /// </summary>
    /// <param name="request"><see cref="CreateUserRequest"/></param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    public Task<UserDto> RegisterUserAsync(CreateUserRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Получение пользователя по идентификатору
    /// </summary>
    /// <param name="request">Идентификатор</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    public Task<UserDto> GetUserByEmail(AuthRequest request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение пользователя по адресу электронной почты
    /// </summary>
    /// <param name="id">Адрес электронной почты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    public Task<UserDto> GetUserByIdAsync(string id, CancellationToken cancellationToken);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public Task<bool> CheckUserByEmailAsync(AuthRequest request, CancellationToken cancellationToken);
}