﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Web.Dto.OrderDtos.OrderOrderingDtos
{
    public class UpdateOrderOrderingDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public decimal TotalPrice { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
