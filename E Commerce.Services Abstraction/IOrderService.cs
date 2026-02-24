using E_Commerce.Shared.CommonResult;
using E_Commerce.Shared.DTOs.OrderDTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services_Abstraction
{
	public interface IOrderService
	{
		// Create Order
		// OrderDTO, Email => OrderToReturnDTO
		Task<Result<OrderToReturnDTO>> CreateOrderAsync(OrderDTO orderDTO, string Email);
		// Get Delivery Methods
		Task<IEnumerable<DeliveryMethodDTO>> GetDeliveryMethodsAsync();

		// Get All Orders For User
		Task<IEnumerable<OrderToReturnDTO>> GetAllOrdersAsync(string Email);
		// Get Order By Id For User
		Task<OrderToReturnDTO> GetOrderByIdAsync(Guid id);
	}
}
