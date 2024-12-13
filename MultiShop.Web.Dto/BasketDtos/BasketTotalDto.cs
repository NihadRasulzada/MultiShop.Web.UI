namespace MultiShop.Web.Dto.BasketDtos
{
    public class BasketTotalDto
    {
        public string UserId { get; set; } 
        public string DiscountCode { get; set; }
        public int DiscountRate { get; set; }
        public List<BasketItemDto> BasketItems { get; set; }
        public decimal Tax { get => TotalPrice / 100 * 10; }
        public decimal TotalPrice { get => BasketItems.Sum(x => x.Price * x.Quantity); }
        public decimal TotalPriceWithTax { get => TotalPrice + Tax; }
        public decimal TotalNewPriceWithDiscount { get => TotalPriceWithTax - (TotalPriceWithTax / 100 * DiscountRate); }
    }
}
