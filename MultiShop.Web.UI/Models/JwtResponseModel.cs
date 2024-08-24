namespace MultiShop.Web.UI.Models
{
    public class JwtResponseModel
    {
        public string AccessToken { get; set; }

        public DateTime AccessTokenExpiration { get; set; }
    }
}
