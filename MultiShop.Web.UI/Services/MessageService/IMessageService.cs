using MultiShop.Web.Dto.MessageDtos;

namespace MultiShop.Web.UI.Services.MessageService
{
    public interface IMessageService
    {
        Task<List<ResultInboxMessageDto>?> GetInboxMessageAsync(string userId);
        Task<List<ResultSendboxMessageDto>?> GetSendboxMessageAsync(string userId);
        Task<int> GetTotalMessageCountByReceiverIdAsync(string userId);
    }
}
