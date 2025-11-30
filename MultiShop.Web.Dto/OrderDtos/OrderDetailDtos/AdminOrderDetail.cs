using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Web.Dto.OrderDtos.OrderDetailDtos
{
    public class AdminOrderDetail
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductAmount { get; set; }
        public string? ProductImageLink { get; set; } = "";
        public decimal ProductTotalPrice { get => ProductAmount * ProductPrice; }
        public int OrderingId { get; set; }
    }
}
