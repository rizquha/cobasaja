using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using HR_App.Models;

namespace HR_App.Scheduler
{
    public class BirthdayService : IHostedService
    {
         private readonly IServiceScopeFactory _scopeFactory;
        public BirthdayService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync");
            using (var scope = _scopeFactory.CreateScope())
            {
                var DB = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                var Emp = (from i in DB.employees select i).FirstOrDefault();
                if(Emp.BirthDate.Day==DateTime.Now.Day && Emp.BirthDate.Month==DateTime.Now.Month )
                {
                    Task.Run(TaskRoutine, cancellationToken);
                }
                else
                {
                    Task.Run(Dont, cancellationToken);
                }
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Sync Task stopped");
            return Task.CompletedTask;
        }

        public Task TaskRoutine()
        {
            
            while (true)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var DB = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                    var Emp = from i in DB.employees where (i.BirthDate.Day==DateTime.Now.Day && i.BirthDate.Month==DateTime.Now.Month) select i;
                    foreach (var i in Emp)
                    {
                    var message = new MimeMessage();
                    message.To.Add(new MailboxAddress(i.Name, i.Email));
                    message.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                    message.Subject = "HAPPY BIRTHDAY " +i.Name;
                    message.Body = new TextPart("plain")
                    {
                        Text = "Wish You Always Happy and Healthy.\n"
                                +"Met Ya Bro !!!\n"
                                +"Terusin Kerjaannye :)"
                    };
                    using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
                    {
                        emailClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                        emailClient.Connect("smtp.mailtrap.io", 587, false);
				        emailClient.Authenticate ("785fd04dea6d9c", "6057ae43ba12a4");
                        emailClient.Send(message);
                        emailClient.Disconnect(true);
                    }
                }
                }
                //Wait 10 minutes till next execution
                DateTime nextStop = DateTime.Now.AddDays(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }

        public Task Dont()
        {
            
            while (true)
            {
                Console.WriteLine("NOT HAD BHIRTDAY");
                //Wait 10 minutes till next execution
                DateTime nextStop = DateTime.Now.AddDays(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }
        
    }
}