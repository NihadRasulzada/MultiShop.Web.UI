using Microsoft.AspNetCore.Http;
using MultiShop.Web.Dto.CatalogDtos.CategoryDtos;

namespace MultiShop.Web.Dto.CatalogDtos.ProductDtos
{
    public class ResultProductWithCategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public IFormFile Photo { get; set; }
        public string ImageLink { get; set; }
        public string Description { get; set; }
        public ResultCategoryDto Category { get; set; }

    }
}
