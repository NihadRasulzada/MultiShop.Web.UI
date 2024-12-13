using Microsoft.AspNetCore.Http;

namespace MultiShop.Web.Dto.CatalogDtos.ProductDtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageName { get; set; }
        public IFormFile Photo { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
    }
}
