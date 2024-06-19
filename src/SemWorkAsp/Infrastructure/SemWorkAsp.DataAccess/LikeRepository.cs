using AutoMapper;
using Infrastructure;
using SemWorkAsp.AppServices.Repositories;
using SemWorkAsp.Contracts.Dtos;
using SemWorkAsp.Domain.Entities;

namespace SemWorkAsp.DataAccess;

public class LikeRepository: ILikeRepository
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public LikeRepository(IMapper mapper, ApplicationDbContext context)
    {
        _context = context;
        _mapper = mapper;
    }


    public async Task AddLikeAsync(Like like, CancellationToken cancellationToken)
    {
        await _context.Likes.AddAsync(like, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLikeAsync(Like like, CancellationToken cancellationToken)
    {
        _context.Likes.Remove(like);
        await _context.SaveChangesAsync(cancellationToken);
    }
}