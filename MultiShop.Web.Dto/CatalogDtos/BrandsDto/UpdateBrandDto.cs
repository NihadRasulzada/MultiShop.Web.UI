﻿using Microsoft.AspNetCore.Http;

namespace MultiShop.Web.Dto.CatalogDtos.BrandsDto
{
    public class UpdateBrandDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ImageName { get; set; }
        public IFormFile Photo { get; set; }

    }
}
