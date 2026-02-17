using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.Services.Exceptions
{
	public abstract class NotFoundException(string Message) : Exception(Message)
	{	
	}
	public sealed class ProductNotFoundException(int Id) : NotFoundException($"Product With Id {Id} Not Found");
	public sealed class BasketNotFoundException(string Id) : NotFoundException($"Basket With Id {Id} Not Found");
}
