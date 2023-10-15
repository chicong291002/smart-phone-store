using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartPhoneStore.Application.Emails
{
    public interface IEmailService
    {
        void Send(string from, string to, string subject, string html);
    }
}
