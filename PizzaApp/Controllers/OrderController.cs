using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Data;
using PizzaApp.DTOs;
using PizzaApp.Entities;
using PizzaApp.Interfaces;

namespace PizzaApp.Controllers;

public class OrderController : BaseApiController
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    [HttpPost("create-order")]
    public async Task<Order> CreateOrder(OrderDto orderDto)
    {
        return await _unitOfWork.OrderRepository.CreateOrder(orderDto);
    }

}