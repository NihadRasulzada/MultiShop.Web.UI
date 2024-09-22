using System.ComponentModel.DataAnnotations;

namespace MultiShop.Web.Dto.AuthDtos.LoginDtos
{
    public class CreateLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
