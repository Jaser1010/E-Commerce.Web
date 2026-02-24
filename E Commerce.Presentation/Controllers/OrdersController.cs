using E_Commerce.Services_Abstraction;
using E_Commerce.Shared.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace E_Commerce.Presentation.Controllers
{
	[Authorize]
	public class OrdersController : ApiBaseController
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}


		// Create Order	
		[HttpPost] // POST: api/orders
		public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
		{
			var email = User.FindFirst(ClaimTypes.Email);
			var result = await _orderService.CreateOrderAsync(orderDTO, email!.Value);
			return Ok(result);
		}

		// Get Delivery Methods
		// GET: api/orders/DeliveryMethods
		[HttpGet("DeliveryMethods")]
		[AllowAnonymous]
		public async Task<ActionResult<IEnumerable<DeliveryMethodDTO>>> GetDeliveryMethods()
		{
			var result = await _orderService.GetDeliveryMethodsAsync();
			return Ok(result);
		}


		// Get All Orders By Email
		// GET: api/orders
		[HttpGet]
		public async Task<ActionResult<IEnumerable<OrderToReturnDTO>>> GetAllOrders()
		{
			var email = User.FindFirst(ClaimTypes.Email);
			var result = await _orderService.GetAllOrdersAsync(email!.Value);
			return Ok(result);
		}




		// Get Order By Id
		// GET: api/orders/{id}
		[HttpGet("{id:guid}")]
		public async Task<ActionResult<OrderToReturnDTO>> GetOrderById(Guid id)
		{
			var result = await _orderService.GetOrderByIdAsync(id);
			return Ok(result);
		}
	}
}
