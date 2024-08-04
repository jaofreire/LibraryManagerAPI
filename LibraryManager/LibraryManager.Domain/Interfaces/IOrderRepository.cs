
using LibraryManager.Domain.Models;
using System.Linq.Expressions;

namespace LibraryManager.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<OrderModel> Register(OrderModel model);
        Task<List<OrderModel>> GetAll();
        Task<List<OrderModel>> GetByTerm(Expression<Func<OrderModel, bool>> predicate);
        Task<List<OrderModel>> GetByTermAsNoTracking(Expression<Func<OrderModel, bool>> predicate);
        Task<OrderModel> Update(OrderModel model);
        Task<bool> Delete(OrderModel model);
    }
}
