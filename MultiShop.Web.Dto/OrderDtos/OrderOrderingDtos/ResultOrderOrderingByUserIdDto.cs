namespace MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos
{
    public class ResultOrderOrderingByUserIdDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
