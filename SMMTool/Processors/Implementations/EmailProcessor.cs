﻿using SMMTool.DTO;
using SMMTool.Utils.Extensions;
using System.Net;
using System.Net.Mail;

namespace SMMTool.Processors.Implementations
{
    internal class EmailProcessor : IProcessor
    {
        private readonly MailMessageDto _messageDto;

        public EmailProcessor(MailMessageDto messageDto)
        {
            _messageDto = messageDto;
        }

        public void Send() =>
            CreateMessages()
                .ForEach(msg =>
                           msg.Use((msg) => CreateSmtpClient()
                                                       .Send(msg)));

        #region Private Methods

        private SmtpClient CreateSmtpClient() =>
            new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(
                                            _messageDto.FromAddress,
                                            _messageDto.Password)
            };

        private IEnumerable<MailMessage> CreateMessages() =>
            _messageDto
               .Addresses
               .Select(toAddress =>
                     new MailMessage(
                              _messageDto.FromAddress,
                              toAddress,
                              _messageDto.Subject,
                              _messageDto.Body));

        #endregion

    }
}
