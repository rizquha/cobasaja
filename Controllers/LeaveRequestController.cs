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
    public class LeaveRequestController : Controller
    {
        private readonly ILogger<LeaveRequestController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public LeaveRequestController(ILogger<LeaveRequestController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        public IActionResult ListLeaveRequest(string search, string val,int? page, int PerPage)
        {
            Console.WriteLine(search);
            Console.WriteLine(val);
            if(!String.IsNullOrEmpty(val) || !String.IsNullOrWhiteSpace(val))
            {
                ViewBag.Message = val;
                ViewBag.search = search;
            }
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            ViewBag.admin=sub;

            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;

            PerPage=5;
            ViewBag.PerPage = PerPage;
            if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
            {
                System.Console.WriteLine("search");
                if(val=="Home")
                {
                    var leaverequest = from i in _AppDbContext.leaveRequests where (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.leaverequest = leaverequest;
                    var leaverequestpending = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
                    ViewBag.leaverequestpending = leaverequestpending;
                    var leaverequestapproved = from i in _AppDbContext.leaveRequests where i.Status=="Approved" select i;
                    ViewBag.leaverequestapproved = leaverequestapproved;
                    var leaverequestrejected = from i in _AppDbContext.leaveRequests where i.Status=="Rejected" select i;
                    ViewBag.leaverequestrejected = leaverequestrejected;
                    var pager = new Pager(leaverequest.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        LeaveRequests = leaverequest.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="News")
                {
                    var leaverequest = from i in _AppDbContext.leaveRequests select i;
                    ViewBag.leaverequest = leaverequest;
                    var leaverequestpending = from i in _AppDbContext.leaveRequests where i.Status=="Pending" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.leaverequestpending = leaverequestpending;
                    var leaverequestapproved = from i in _AppDbContext.leaveRequests where i.Status=="Approved" select i;
                    ViewBag.leaverequestapproved = leaverequestapproved;
                    var leaverequestrejected = from i in _AppDbContext.leaveRequests where i.Status=="Rejected" select i;
                    ViewBag.leaverequestrejected = leaverequestrejected;
                    var pager = new Pager(leaverequestpending.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        LeaveRequests = leaverequestpending.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="Contact")
                {
                    var leaverequest = from i in _AppDbContext.leaveRequests select i;
                    ViewBag.leaverequest = leaverequest;
                    var leaverequestpending = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
                    ViewBag.leaverequestpending = leaverequestpending;
                    var leaverequestapproved = from i in _AppDbContext.leaveRequests where i.Status=="Approved" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.leaverequestapproved = leaverequestapproved;
                    var leaverequestrejected = from i in _AppDbContext.leaveRequests where i.Status=="Rejected" select i;
                    ViewBag.leaverequestrejected = leaverequestrejected;
                    var pager = new Pager(leaverequestapproved.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        LeaveRequests = leaverequestapproved.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);

                   
                }
                else if(val=="About")
                {
                   
                    var leaverequest = from i in _AppDbContext.leaveRequests select i;
                    ViewBag.leaverequest = leaverequest;
                    var leaverequestpending = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
                    ViewBag.leaverequestpending = leaverequestpending;
                    var leaverequestapproved = from i in _AppDbContext.leaveRequests where i.Status=="Approved" select i;
                    ViewBag.leaverequestapproved = leaverequestapproved;
                    var leaverequestrejected = from i in _AppDbContext.leaveRequests where i.Status=="Rejected" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.leaverequestrejected = leaverequestrejected;
                    var pager = new Pager(leaverequestrejected.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        LeaveRequests = leaverequestrejected.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
            }else{
                var leaverequest = from i in _AppDbContext.leaveRequests select i;
                ViewBag.leaverequest = leaverequest;
                var leaverequestpending = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
                ViewBag.leaverequestpending = leaverequestpending;
                var leaverequestapproved = from i in _AppDbContext.leaveRequests where i.Status=="Approved" select i;
                ViewBag.leaverequestapproved = leaverequestapproved;
                var leaverequestrejected = from i in _AppDbContext.leaveRequests where i.Status=="Rejected" select i;
                ViewBag.leaverequestrejected = leaverequestrejected;
                var pager = new Pager(leaverequest.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        LeaveRequests = leaverequest.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
            }
            
            return View();
        }
        public IActionResult Detail(int id)
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            var details = from i in _AppDbContext.leaveRequests where i.Id==id select i;
            ViewBag.details = details;
            return View();
        }
        public IActionResult ExportCSV(string val,string search)
        {
            System.Console.WriteLine("EXPORT");
            System.Console.WriteLine(val);
            System.Console.WriteLine(search);
            var status ="";
            if(val=="News")
            {
                status="Pending";
            }else if(val=="Contact")
            {
                status="Approved";
            }else if(val=="About")
            {
                status="Rejected";
            }
            Console.WriteLine("STATUS");
            Console.WriteLine(status);

            var comlumHeadrs = new string[]
            {
                "Id",
                "Name",
                "Email",
                "Phone",
                "Position",
                "Departement",
                "Leave Date",
                "Reason"
            };
            var items= new List<object[]>();
            if(val=="Home")
            {
                if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
                {
                    items = (from item in _AppDbContext.leaveRequests where item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search)
                            select new object[]
                            {
                                        item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.LeaveDate}",
                                        $"{item.reason}"
                            }).ToList();
                }
                else
                {
                    items = (from item in _AppDbContext.leaveRequests
                         select new object[]
                         {
                                       item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.LeaveDate}",
                                        $"{item.reason}"
                         }).ToList();
                }
            }else
            {
                Console.WriteLine("NOTHOME");
                if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
                {
                    Console.WriteLine("NOT HOME SEARCH NOT");
                    items = (from item in _AppDbContext.leaveRequests where item.Status==status && (item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search))
                         select new object[]
                         {
                                       item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.LeaveDate}",
                                        $"{item.reason}"
                         }).ToList();
                }else
                {
                    Console.WriteLine("NOT HOME SEARCH");
                    items = (from item in _AppDbContext.leaveRequests where item.Status==status
                         select new object[]
                         {
                                        item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.LeaveDate}",
                                        $"{item.reason}"
                         }).ToList();
                }
            }

            var itemcsv = new StringBuilder();
            items.ForEach(line =>
                {
                    itemcsv.AppendLine(string.Join(",", line));
                });

            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{itemcsv.ToString()}");
            return File(buffer, "text/csv", $"AllLeaveRequest.csv");            
        }
        public IActionResult Export()
        {
            var comlumHeadrs = new string[]
            {
                "Id",
                "Name",
                "Email",
                "Phone",
                "Position",
                "Departement",
                "Leave Date",
                "Reason"
            };
            var items = (from item in _AppDbContext.leaveRequests
                         select new object[]
                         {
                                       item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.LeaveDate}",
                                        $"{item.reason}"
                         }).ToList();

            var itemcsv = new StringBuilder();
            items.ForEach(line =>
                {
                    itemcsv.AppendLine(string.Join(",", line));
                });

            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{itemcsv.ToString()}");
            return File(buffer, "text/csv", $"AllLeaveRequest.csv");
        }
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DELETE");
            Console.WriteLine(id);
            var request = _AppDbContext.leaveRequests.Find(id);
            _AppDbContext.leaveRequests.Remove(request);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListLeaveRequest","LeaveRequest");
        }
        public IActionResult NewLeaveReq()
        {
            return View();
        }
        public IActionResult Approved(int id)
        {
            var request = _AppDbContext.leaveRequests.Find(id);
            var nama = request.Name;
            var email = request.Email;
            var leavedate = request.LeaveDate;
            var status ="Approved";
            SendEmail(nama,email,leavedate,status);
            request.Status="Approved";
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListLeaveRequest","LeaveRequest");
        }

        private void SendEmail(string nama, string email, DateTime leavedate,string status)
        {
                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                msg.To.Add(new MailboxAddress(nama,email));
                msg.Subject ="Update Your Leave Request";
                msg.Body = new TextPart("Plain")
                {
                    Text = "Hai "+nama+",\n"+"Selamat Status Leave Request Pada Tanggal "+leavedate.Date+".\n"+"Telah Berubah Status Menjadi "+status
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

        public IActionResult Rejected(int id)
        {
            var request = _AppDbContext.leaveRequests.Find(id);
            request.Status="Rejected";
            var nama = request.Name;
            var email = request.Email;
            var leavedate = request.LeaveDate;
            var status ="Approved";
            SendEmail(nama,email,leavedate,status);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListLeaveRequest","LeaveRequest");
        }

        
        
        
    }
}