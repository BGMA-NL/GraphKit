using GraphKit.Async;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Graph;
using Microsoft.Graph.Models;
using Microsoft.Graph.Users.Item.SendMail;
using Microsoft.Kiota.Abstractions;

namespace GraphKit.Core
{
    public class EmailService: IEmailService, IEmailSender
    {
        private readonly IGraphKitProvider _graphProvider;

        public EmailService(IGraphKitProvider provider)
        {
            _graphProvider = provider;
        }
        
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            return SendAsync(email, subject, htmlMessage, true);
        }

        public GraphResponse Send(string mailTo, string subject, string message, bool isHtml = false)
        {
            return AsyncHelpers.RunSync(() => SendAsync(mailTo, Array.Empty<string>(), Array.Empty<string>(), subject, message, Array.Empty<Infrastructure.Internal.Attachment>(), isHtml))!;
        }

        public GraphResponse Send(string mailTo, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false)
        {
            return AsyncHelpers.RunSync(() => SendAsync(mailTo, Array.Empty<string>(), Array.Empty<string>(), subject, message, attachments, isHtml))!;
        }

        public GraphResponse Send(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, bool isHtml = false)
        {
            return AsyncHelpers.RunSync(() => SendAsync(mailTo, mailCc, mailBcc, subject, message, Array.Empty<Infrastructure.Internal.Attachment>(), isHtml))!;
        }

        public GraphResponse Send(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false)
        {
            return AsyncHelpers.RunSync(() => SendAsync(mailTo, mailCc, mailBcc, subject, message, attachments, isHtml))!;
        }

        public Task<GraphResponse> SendAsync(string mailTo, string subject, string message, bool isHtml = false)
        {
            return SendAsync(mailTo, Array.Empty<string>(), Array.Empty<string>(), subject, message, Array.Empty<Infrastructure.Internal.Attachment>(), isHtml);
        }

        public Task<GraphResponse> SendAsync(string mailTo, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false)
        {
            return SendAsync(mailTo, Array.Empty<string>(), Array.Empty<string>(), subject, message, attachments, isHtml);
        }

        public Task<GraphResponse> SendAsync(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, bool isHtml = false)
        {
            return SendAsync(mailTo, mailCc, mailBcc, subject, message, Array.Empty<Infrastructure.Internal.Attachment>(), isHtml);
        }

        public async Task<GraphResponse> SendAsync(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false)
        {
            var recipient = new List<Recipient> () { new Recipient{
                    EmailAddress = new EmailAddress
                    {
                        Address = mailTo

                    }
            }};

            var ccRecipients = mailCc.Select(x => new Recipient{
                EmailAddress = new EmailAddress
                    {
                        Address = x

                    }
            });

            var bccRecipients = mailBcc.Select(x => new Recipient{
                EmailAddress = new EmailAddress
                    {
                        Address = x

                    }
            });

            var  mailAttachments = attachments.Select(attachment => 
                new FileAttachment { Name = attachment.FileName, ContentBytes = Convert.FromBase64String(attachment.FileBase64) }).Cast<Attachment>().ToList();


            var emailMessage = new Message
            {
                Subject = subject,
                Body = new ItemBody
                {
                    ContentType = isHtml ? BodyType.Html : BodyType.Text,
                    Content = message,
                },
                ToRecipients = recipient,
                CcRecipients = ccRecipients.ToList(),
                BccRecipients = bccRecipients.ToList(),
                Attachments = mailAttachments
            };

            var body = new SendMailPostRequestBody()
            {
                Message = emailMessage,
                SaveToSentItems = false
            };
            
            var nativeResponseHandler = new NativeResponseHandler();
            await _graphProvider.GraphClient.Users[_graphProvider.SenderEmail].SendMail.PostAsync(body, requestConfiguration => requestConfiguration.Options.Add(new ResponseHandlerOption(){ ResponseHandler = nativeResponseHandler }));

            var responseMessage = nativeResponseHandler.Value as HttpResponseMessage;
            if (responseMessage is null)  throw new ArgumentNullException(nameof(responseMessage));
            var locationHeader = responseMessage.Headers.Location;
            var gotRequestMethod = Enum.TryParse<Method>(responseMessage.RequestMessage?.Method.Method ?? "", out var requestMethod);
            var requestUri = responseMessage.RequestMessage?.RequestUri;
            var requestHeaders =
                responseMessage.RequestMessage?.Headers.ToDictionary(a => a.Key, a => a.Value) ?? new();

            var headers = new RequestHeaders();

            foreach (var header in requestHeaders)
            {
                headers.Add(header.Key, header.Value);
            }

            var requestInformation = new RequestInformation()
            {
                HttpMethod = requestMethod,
                URI = requestUri ?? new Uri("/"),
            };
            
            requestInformation.AddHeaders(headers);
            
            return new GraphResponse(requestInformation, responseMessage);
        }
    }
}
