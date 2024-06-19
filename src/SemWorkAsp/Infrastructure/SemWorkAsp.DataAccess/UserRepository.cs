using AutoMapper;
using AutoMapper.QueryableExtensions;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using SemWorkAsp.AppServices.Repositories;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess;

/// <summary>
/// <inheritdoc/>
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly IRepository<User> _repository;
    private readonly IMapper _mapper;

    public UserRepository(IRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync(CancellationToken cancellationToken)
    {
        return await _repository.GetAll().ProjectTo<UserDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
    }

    public async Task<UserDto> AddUserAsync(User request, CancellationToken cancellationToken)
    {
        var userFromDb = await _repository.AddAsync(request, cancellationToken);
        return _mapper.Map<UserDto>(userFromDb);
    }

    public async Task<UserDto> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = _repository.GetAll().Where(u => u.Email == email).FirstAsync(cancellationToken).Result;
        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(Guid.Parse(id));
        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> CheckUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var users = _repository.GetAll().Where(u => u.Email == email).ToArrayAsync(cancellationToken).Result;
        return users.Length >= 1;
    }
}