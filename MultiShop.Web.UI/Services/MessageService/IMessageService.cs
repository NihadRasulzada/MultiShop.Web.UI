using MultiShop.Web.Dto.MessageDtos;

namespace MultiShop.Web.UI.Services.MessageService
{
    /// <summary>
    /// Provides methods for retrieving and managing messages.
    /// </summary>
    public interface IMessageService
    {
        /// <summary>
        /// Retrieves the inbox messages for the specified user ID.
        /// </summary>
        /// <param name="id">The user ID whose inbox messages are to be retrieved.</param>
        /// <returns>A list of inbox messages, or null if no messages are found.</returns>
        Task<List<ResultInboxMessageDto>?> GetInboxMessageAsync(string userId);

        /// <summary>
        /// Retrieves the sendbox messages for the specified user ID.
        /// </summary>
        /// <param name="id">The user ID whose sendbox messages are to be retrieved.</param>
        /// <returns>A list of sendbox messages, or null if no messages are found.</returns>
        Task<List<ResultSendboxMessageDto>?> GetSendboxMessageAsync(string userId);

        /// <summary>
        /// Gets the total count of messages received by a specific user ID.
        /// </summary>
        /// <param name="id">The user ID to count received messages for.</param>
        /// <returns>The total count of messages received by the user.</returns>
        Task<int> GetTotalMessageCountByReceiverIdAsync(string userId);
    }
}
