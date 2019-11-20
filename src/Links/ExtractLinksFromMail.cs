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

            Links.Set(context, LinksHelper.ExtractLinks(mailMessage.Body));
        }
    }

    public class ExtractLinksFromMessage : CodeActivity
    {
        [Category("Input")]
        [RequiredArgument]
        public InArgument<string> Message { get; set; }

        [Category("Output")]
        public OutArgument<IList<string>> Links { get; set; }


        protected override void Execute(CodeActivityContext context)
        {
            var message = Message.Get(context);
            if (string.IsNullOrEmpty(message))
                throw new ArgumentNullException("Message is empty");

            Links.Set(context, LinksHelper.ExtractLinks(message));
        }
    }
}
