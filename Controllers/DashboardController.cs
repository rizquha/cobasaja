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
namespace HR_App.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public DashboardController(ILogger<DashboardController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }

        [Authorize]
        public IActionResult HomeDashboard()
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var employee = from i in _AppDbContext.employees select i;
            var count = employee.Count();
            ViewBag.EmpCount=count;

            var maleEmp = from i in _AppDbContext.employees where i.Gender=="Male" select i;
            var countMale = maleEmp.Count();
            ViewBag.countMale = countMale;

            var attendance = from i in _AppDbContext.attendance where i.PresenceIn.Date ==DateTime.Now.Date select i;
            var countAttendance = attendance.Count();
            ViewBag.attendanceTotal = countAttendance;

            var leave = from i in _AppDbContext.leaveRequests where i.LeaveDate.Date==DateTime.Now.Date select i;
            var leaveCount = leave.Count();
            ViewBag.countLeave =leaveCount;

            var events = (from i in _AppDbContext.events select i).OrderBy(x=>x.eventDate).Take(5);
            ViewBag.events = events;

            var newApplicants = (from i in _AppDbContext.applicants select i).OrderBy(i=>i.CreatedAt).Take(5);
            ViewBag.newApplicants = newApplicants;

            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            return View();
        }
        public IActionResult AddEvent(DateTime date, string name)
        {
            var add= new Events()
            {
                eventDate = date,
                eventName = name
            };
            _AppDbContext.events.Add(add);
            _AppDbContext.SaveChanges();
            return RedirectToAction("HomeDashboard","Dashboard");
        }
        [Authorize]
        public IActionResult Event()
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            var eventCompany = from i in _AppDbContext.events select i;
            ViewBag.eventCompany = eventCompany;
            return View();
        }
        public IActionResult Delete(int id)
        {
            var ev = _AppDbContext.events.Find(id);
            _AppDbContext.events.Remove(ev);
            _AppDbContext.SaveChanges();
            return RedirectToAction("Event","Dashboard");
        }
        public IActionResult SaveEdit(int id, DateTime date, string name)
        {
            var x = _AppDbContext.events.Find(id);
            x.eventDate = date;
            x.eventName=name;
            _AppDbContext.SaveChanges();
            return RedirectToAction("Event","Dashboard");
        }
        
    }
}