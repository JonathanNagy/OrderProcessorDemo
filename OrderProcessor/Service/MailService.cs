using OrderProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Service
{
    public class MailService : IMailService
    {
        public ServiceResponse SendMail(string recipient, string from, string subject, string body)
        {
            throw new NotImplementedException();
        }
    }
}
