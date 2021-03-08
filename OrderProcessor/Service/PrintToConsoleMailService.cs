using OrderProcessor.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessor.Service
{
    public class PrintToConsoleMailService : IMailService
    {
        public ServiceResponse SendMail(string recipient, string from, string subject, string body)
        {
            Console.WriteLine("Sending email...");
            Console.WriteLine($"To: {recipient}\n" + 
                              $"From: {from}\n" + 
                              $"Subject: {subject}\n" +
                              $"{body}");

            return new ServiceResponse()
            {
                Success = true
            };
        }
    }
}
