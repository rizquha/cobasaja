using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HR_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using MimeKit;

namespace HR_App.Controllers
{
    public class BroadcastController : Controller
    {
        private readonly ILogger<BroadcastController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public BroadcastController(ILogger<BroadcastController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }

        [Authorize]
        public IActionResult Send()
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            return View();
        }
        public IActionResult SendBroadcast( string title, string message)
        {
            var msg = new MimeMessage();
            var user = from i in _AppDbContext.employees.OrderBy(x=>x.Id) select i;
            foreach(var i in user)
            {
                var nama = i.Name;
                var emailAddress = i.Email;
                msg.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                msg.To.Add(new MailboxAddress(nama,emailAddress));
                msg.Subject =title;
                msg.Body = new TextPart("Plain")
                {
                    Text = message
                };
                using(var client = new MailKit.Net.Smtp.SmtpClient())
                {
                client.ServerCertificateValidationCallback = (s,c,h,e) => true;
				client.Connect ("smtp.mailtrap.io", 587, false);
				client.Authenticate ("785fd04dea6d9c", "6057ae43ba12a4");
				client.Send (msg);
				client.Disconnect (true);
                }
            }
            return RedirectToAction("Send","Broadcast");
        }
    }
}