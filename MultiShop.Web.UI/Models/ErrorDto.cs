namespace MultiShop.Web.UI.Models
{
    public class ErrorDto
    {
        public List<String> Errors { get; private set; } = new List<string>();
        public bool IsShow { get; private set; }
    }
}
