namespace E_Commerce.Shared.DTOs.OrderDTOs
{
	public record OrderItemDTO(string ProductName, string PictureUrl, decimal Price, int Quantity);
}