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
    public class AttendanceController : Controller
    {
        private readonly ILogger<AttendanceController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public AttendanceController(ILogger<AttendanceController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        public IActionResult Index(string search)
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
            {
                var present = (from i in _AppDbContext.employees from x in i.attendance where (x.PresenceIn.Date==DateTime.Now.Date) && i.Name.Contains(search) select i).Distinct();
                ViewBag.present = present;
                var absent = (from i in _AppDbContext.employees from x in i.attendance where (x.PresenceIn.Date !=DateTime.Now.Date) && i.Name.Contains(search) select i).Distinct();
                ViewBag.absent = absent;
            }else
            {
                var present = (from i in _AppDbContext.employees from x in i.attendance where x.PresenceIn.Date==DateTime.Now.Date select i).OrderBy(z=>z.Id);
                ViewBag.present = present;
                List<int> presentToday = new List<int>();
                foreach(var i in present)
                {
                    presentToday.Add(i.Id);
                }
                ViewBag.todaypresent = presentToday;
                var absent = (from i in _AppDbContext.employees select i).OrderBy(x=>x.Id);
                ViewBag.absentall=absent;
                List<int> NotpresentToday = new List<int>();
                foreach(var i in absent)
                {
                    NotpresentToday.Add(i.Id);
                }
                var listabsent = NotpresentToday.Except(presentToday).ToList();
                ViewBag.absent= listabsent;
                ViewBag.countabsent=listabsent.Count();
            }
            return View();
        }
        [Authorize]
        public IActionResult DetailAttendance(int id)
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            var name = from i in _AppDbContext.employees where i.Id==id select i;
            ViewBag.name = name;

            var attendance = (from i in _AppDbContext.attendance where i.employees.Id==id select i).OrderByDescending(i=>i.PresenceIn.Date);
            ViewBag.attendance = attendance;
            return View();
        }
    }
}