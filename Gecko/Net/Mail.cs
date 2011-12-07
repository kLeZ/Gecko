using System;
using System.Net.Mail;
using System.Text;

namespace Gecko.Net
{
	using System.Net;
	using Extensions.CollectionExtensions;

	public class Mail
	{
		/// <summary>
		/// Invia un messaggio di posta elettronica
		/// </summary>
		/// <param name="MessageFrom">Mittente</param>
		/// <param name="MessageTo">Destinatario o destinatari</param>
		/// <param name="MessageCc">Destinatario o destinatari in copia conoscenza</param>
		/// <param name="MessageBcc">Destinatario o destinatari in copia conoscenza nascosta</param>
		/// <param name="MessageSubject">Soggetto del messaggio</param>
		/// <param name="MessagePriority">Priorità del messaggio</param>
		/// <param name="MessageText">Testo del messaggio</param>
		/// <param name="ServerName">Server di email</param>
		public static void SendMail(string MessageFrom, string MessageTo, string MessageCc, string MessageBcc, string MessageSubject, MailPriority MessagePriority, string MessageText, string ServerName, bool EnableSsl, NetworkCredential authentication)
		{
			MailMessage myMail = SetBasicInfos(MessageFrom, MessageTo, MessageCc, MessageBcc, MessageSubject, MessagePriority, MessageText);
			SmtpClient mySmtpC = null;
			if (ServerName != null)
				mySmtpC = new SmtpClient(ServerName);
			else
				mySmtpC = new SmtpClient();

			mySmtpC.EnableSsl = EnableSsl;
			if (authentication != null)
				mySmtpC.Credentials = authentication;
			mySmtpC.Send(myMail);
		}

		private static MailMessage SetBasicInfos(string MessageFrom, string MessageTo, string MessageCc, string MessageBcc, string MessageSubject, MailPriority MessagePriority, string MessageText)
		{
			char[] delim = { ',', ';', ' ', ':', '\\', '|' };
			StringSplitOptions opt = StringSplitOptions.RemoveEmptyEntries;
			MailMessage myMail = new MailMessage();

			myMail.From = new MailAddress(MessageFrom);
			myMail.To.AddList(MessageTo.Split(delim, opt).Encapsulate<string, MailAddress>());

			if (!String.IsNullOrEmpty(MessageCc))
				myMail.CC.AddList<MailAddress>(
					MessageCc.Split(delim, opt)
					.Encapsulate<string, MailAddress>()
					);

			if (!String.IsNullOrEmpty(MessageBcc))
				myMail.Bcc.AddList<MailAddress>(
					MessageCc.Split(delim, opt)
					.Encapsulate<string, MailAddress>()
					);

			myMail.Subject = MessageSubject;
			myMail.Priority = MessagePriority;
			myMail.BodyEncoding = Encoding.UTF8;
			myMail.Body = MessageText;
			return myMail;
		}
	}
}
