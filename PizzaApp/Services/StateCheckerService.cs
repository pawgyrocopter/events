using Microsoft.EntityFrameworkCore;
using PizzaApp.Data;
using PizzaApp.Entities;

namespace PizzaApp.Services;

public class StateCheckerService
{
    private readonly DataContext _context;

    public StateCheckerService(DataContext context)
    {
        _context = context;
    }

    public async Task<bool> ChangeOrderStateToReady(int orderId)
    {
        var order = await _context.Orders.Include(p => p.Pizzas).FirstOrDefaultAsync(x => x.Id == orderId);
        var answer = order.Pizzas.All(x => x.State == State.Ready);
        if (answer) order.OrderState = OrderState.Ready;
        await _context.SaveChangesAsync();
        return answer;
    }
}