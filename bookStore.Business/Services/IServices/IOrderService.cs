using bookStore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace bookStore.Business.Services.IServices
{
    public interface IOrderService
    {
        Task<OrderHeader> CreateOrderAsync(OrderHeader orderHeader);

        Task<OrderHeader?> GetOrderByIdAsync(int id, bool includeUser = false,bool includeDetails=false);

        Task<IEnumerable<OrderHeader>> GetAllOrdersAsync(string? userId = null, string? status = null, bool includeUser = false, bool includeDetails = false);

        Task UpdateOrderAsync(OrderHeader orderHeader);

        Task UpdateOrderStatusAsync(int id,string orderStatus, string? carrier = null, string? trackingumber = null);

    }
}