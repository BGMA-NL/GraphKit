using Microsoft.Graph;


namespace GraphKit.Core
{
    public interface IEmailService
    {
        /// <summary>
        /// send email with UTF-8
        /// </summary>
        /// <param name="mailTo">consignee email,multi split with ","</param>
        /// <param name="subject">subject</param>
        /// <param name="message">email message</param>
        /// <param name="isHtml">is set message as html</param>
        /// <returns>GraphResponse</returns>
        GraphResponse Send(string mailTo, string subject, string message, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8
        /// </summary>
        /// <param name="mailTo">consignee email,multi split with ","</param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <param name="isHtml"></param>
        /// <returns>GraphResponse</returns>
        GraphResponse Send(string mailTo, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8
        /// </summary>
        /// <param name="mailTo">consignee email,multi split with ","</param>
        /// <param name="mailCc">send cc,multi split with ","</param>
        /// <param name="mailBcc">send bcc,multi split with ","</param>
        /// <param name="subject">subject</param>
        /// <param name="message">email message</param>
        /// <param name="isHtml">is set message as html</param>
        /// <returns>GraphResponse</returns>
        GraphResponse Send(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="mailCc"></param>
        /// <param name="mailBcc"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <param name="isHtml"></param>
        /// <returns>GraphResponse</returns>
        GraphResponse Send(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8 async
        /// </summary>
        /// <param name="mailTo">consignee email,multi split with ","</param>
        /// <param name="subject">subject</param>
        /// <param name="message">email message</param>
        /// <param name="isHtml">is set message as html</param>
        /// <returns>GraphResponse</returns>
        Task<GraphResponse> SendAsync(string mailTo, string subject, string message, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8 async
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="attachments"></param>
        /// <param name="isHtml"></param>
        /// <param name="sender"></param>
        /// <returns>GraphResponse</returns>
        Task<GraphResponse> SendAsync(string mailTo, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false);

        /// <summary>
        /// send email with UTF-8 async
        /// </summary>
        /// <param name="mailTo">consignee email,multi split with ","</param>
        /// <param name="mailCc">send cc,multi split with ","</param>
        /// <param name="mailBcc">send bcc,multi split with ","</param>
        /// <param name="subject">subject</param>
        /// <param name="message">email message</param>
        /// <param name="isHtml">is set message as html</param>
        /// <returns>GraphResponse</returns>
        Task<GraphResponse> SendAsync(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, bool isHtml = false);
        
        /// <summary>
        /// send email UTF-8 async
        /// </summary>
        /// <param name="mailTo"></param>
        /// <param name="mailCc"></param>
        /// <param name="mailBcc"></param>
        /// <param name="subject"></param>
        /// <param name="message"></param>
        /// <param name="isHtml"></param>
        /// <returns>GraphResponse</returns>
        Task<GraphResponse> SendAsync(string mailTo, string[] mailCc, string[] mailBcc, string subject, string message, Infrastructure.Internal.Attachment[] attachments, bool isHtml = false);
    }
}
