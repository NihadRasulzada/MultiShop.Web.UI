namespace MultiShop.Web.Dto.BasketDtos
{
    public class BasketItemDto
    {

        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductImageName { get; set; }
        public string ProductImageLink { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
