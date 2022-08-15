using AutoMapper;
using Domain.DTOs;
using Domain.Interfaces.IRepository;
using Domain.Interfaces.IServices;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Services;

public class TopingService : ITopingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TopingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<TopingDto>> GetTopings()
    {
        return await _mapper.ProjectTo<TopingDto>(await _unitOfWork.TopingRepository.GetTopings()).ToListAsync();
    }

    public async Task<TopingDto> CreateToping(string name)
    {
        var toping = _mapper.Map<TopingDto>(await _unitOfWork.TopingRepository.CreateToping(name));
        await _unitOfWork.Complete();
        return toping;
    }

    public async Task<TopingDto> GetTopingById(int topingId)
    {
        return _mapper.Map<TopingDto>(await _unitOfWork.TopingRepository.GetTopingById(topingId));
    }
}