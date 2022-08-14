using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PizzaApp.DTOs;
using PizzaApp.Interfaces;

namespace PizzaApp.Services;

public class TopingService : ITopingService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public TopingService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ActionResult<IEnumerable<TopingDto>>> GetTopings()
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