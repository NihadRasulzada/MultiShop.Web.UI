﻿namespace MultiShop.Web.Dto.MessageDtos
{
    public class ResultSendboxMessageDto
    {
        public int Id { get; set; }
        public string SenderId { get; set; }
        public string ReceiverId { get; set; }
        public string Subject { get; set; }
        public string MessageDetail { get; set; }
        public bool IsRead { get; set; }
        public DateTime MessageDate { get; set; }

    }
}
