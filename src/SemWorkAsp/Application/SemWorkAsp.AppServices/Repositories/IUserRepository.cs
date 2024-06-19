using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

/// <summary>
/// Репозиторий для пользователей
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Получение всех пользователей
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken);
    
    /// <summary>
    /// Добавление нового пользователя в БД
    /// </summary>
    /// <param name="request">Модель пользователя с фронта</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    Task<UserDto> AddUserAsync(User request, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение пользователя по адресу электронной почты
    /// </summary>
    /// <param name="email">Адрес электронной почты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    public Task<UserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    
    /// <summary>
    /// Получение пользователя по адресу электронной почты
    /// </summary>
    /// <param name="id">Адрес электронной почты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns><see cref="UserDto"/></returns>
    public Task<UserDto> GetUserByIdAsync(string id, CancellationToken cancellationToken);

    /// <summary>
    /// Проверка существования пользователя по адресу электронной почты
    /// </summary>
    /// <param name="email">Адрес электронной почты</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Существует ли такой пользователь</returns>
    public Task<bool> CheckUserByEmailAsync(string email, CancellationToken cancellationToken);
}