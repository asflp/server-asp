using AutoMapper;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Contracts.ModelsRequest;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.AppServices.Repositories;

/// <inheritdoc/>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllUsersAsync(cancellationToken);
    }

    public Task<UserDto> RegisterUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(request);
        return _userRepository.AddUserAsync(entity, cancellationToken);
    }

    public async Task<UserDto> GetUserByEmail(AuthRequest request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByEmailAsync(request.Email, cancellationToken);
    }

    public async Task<UserDto> GetUserByIdAsync(string id, CancellationToken cancellationToken)
    {
        return await _userRepository.GetUserByIdAsync(id, cancellationToken);
    }

    public async Task<bool> CheckUserByEmailAsync(AuthRequest request, CancellationToken cancellationToken)
    {
        return await _userRepository.CheckUserByEmailAsync(request.Email, cancellationToken);
    }
}