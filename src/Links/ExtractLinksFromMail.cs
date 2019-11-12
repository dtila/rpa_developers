using HtmlAgilityPack;
using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Links
{
    public class ExtractLinksFromMail : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<MailMessage> MailMessage { get; set; }

        [Category("Output")]
        public OutArgument<IList<string>> Links { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            var mailMessage = MailMessage.Get(context);
            if (mailMessage.IsBodyHtml)
                throw new InvalidOperationException("The body mail of the message is not formatted as an HTML");

            Links.Set(context, MailLinks.ExtractLinks(mailMessage.Body));
        }
    }

    public class MailLinks
    {
        public static IList<string> ExtractLinks(string body)
        {
            var html = new HtmlDocument();
            try
            {
                html.LoadHtml(body);
                var links = from node in html.DocumentNode.Descendants("a")
                            let href = node.Attributes["href"]
                            where href != null && !string.IsNullOrEmpty(href.Value)
                            select href;
                return links.Select(li => li.Value).ToList();
            }
            catch (Exception ex)
            {
                throw new NotImplementedException(ex.Message);
            }
        }
    }
}
