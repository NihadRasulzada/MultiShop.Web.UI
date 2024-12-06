using MultiShop.Web.Dto.MessageDtos;

namespace MultiShop.Web.UI.Services.MessageService
{
    public class MessageService : IMessageService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://localhost:5000/services/Message/UserMessage/";

        public MessageService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<ResultInboxMessageDto>> GetInboxMessageAsync(string userId)
        {
            ValidateInput(userId, nameof(userId));
            return await SafeGetAsync<List<ResultInboxMessageDto>>($"GetMessageInbox?id={userId}");
        }

        public async Task<List<ResultSendboxMessageDto>> GetSendboxMessageAsync(string userId)
        {
            ValidateInput(userId, nameof(userId));
            return await SafeGetAsync<List<ResultSendboxMessageDto>>($"GetMessageSendbox?id={userId}");
        }

        public async Task<int> GetTotalMessageCountByReceiverIdAsync(string receiverId)
        {
            ValidateInput(receiverId, nameof(receiverId));
            return await SafeGetAsync<int>($"GetTotalMessageCountByReceiverId?id={receiverId}");
        }

        private async Task<T> SafeGetAsync<T>(string endpoint)
        {
            try
            {
                var fullUrl = $"{BaseUrl}{endpoint}";
                var response = await _httpClient.GetAsync(fullUrl);
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadFromJsonAsync<T>();
                if (content == null)
                    throw new InvalidOperationException($"The API response for {endpoint} returned null.");

                return content;
            }
            catch (HttpRequestException ex)
            {
                throw new ApplicationException($"Error fetching data from {endpoint}: {ex.Message}", ex);
            }
            catch (InvalidOperationException ex)
            {
                throw new ApplicationException($"Error processing the response from {endpoint}: {ex.Message}", ex);
            }
        }

        private void ValidateInput(string input, string paramName)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException($"The {paramName} cannot be null, empty, or whitespace.", paramName);
        }
    }
}
