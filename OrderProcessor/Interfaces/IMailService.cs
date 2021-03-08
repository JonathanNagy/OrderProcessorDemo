using OrderProcessor.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Interfaces
{
    public interface IMailService
    {
        ServiceResponse SendMail(string recipient, string from, string subject, string body);
    }
}
