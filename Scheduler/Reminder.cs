using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using HR_App.Models;
using System.Collections.Generic;

namespace HR_App.Scheduler
{
    public class ReminderService : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        public ReminderService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("StartAsync");
            using (var scope = _scopeFactory.CreateScope())
            {
                var DB = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                var reminder = (from i in DB.events select i).FirstOrDefault();
                if(reminder.eventDate.Date.AddDays(-3)==DateTime.Now.Date)
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
            Console.WriteLine("Async Task stopped");
            return Task.CompletedTask;
        }

        public Task TaskRoutine()
        {
            
            while (true)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                var DB = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                var emp = from i in DB.employees select i;
                var reminder = from i in DB.events where i.eventDate.Date.AddDays(-3)==DateTime.Now.Date select i; 
                List<string> nameReminder = new List<string>();
                List<DateTime> dateReminder = new List<DateTime>();
                foreach(var x in reminder)
                {
                    nameReminder.Add(x.eventName);
                    dateReminder.Add(x.eventDate.Date);
                }
                for(int i=0;i<nameReminder.Count();i++)
                {
                    foreach (var x in emp)
                    {
                        var message = new MimeMessage();
                        message.To.Add(new MailboxAddress(x.Name, x.Email));
                        message.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                        message.Subject = "Selamat Hari "+nameReminder[i];
                        message.Body = new TextPart("plain")
                        {
                            Text = "Dalam memperingati Hari "+nameReminder[i]+" maka perusahaan meliburkan semua employee pada tanggal : "+dateReminder[i]
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
                Console.WriteLine("NOT HAD REMINDERS");
                //Wait 10 minutes till next execution
                DateTime nextStop = DateTime.Now.AddDays(1);
                var timeToWait = nextStop - DateTime.Now;
                var millisToWait = timeToWait.TotalMilliseconds;
                Thread.Sleep((int)millisToWait);
            }
        }
    }
}