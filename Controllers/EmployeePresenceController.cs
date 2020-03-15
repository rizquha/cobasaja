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
    public class EmployeePresenceController : Controller
    {
        private readonly ILogger<EmployeePresenceController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public EmployeePresenceController(ILogger<EmployeePresenceController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        public IActionResult Clock()
        {
            System.Console.WriteLine("Clock");
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;

            var iduser=0;
            var id = from i in _AppDbContext.employees where i.Username == sub select i.Id;
            foreach(var i in id)
            {
                iduser = i;
            }
            ViewBag.iduser = iduser;
            var attendance = from i in _AppDbContext.attendance where i.employees.Id == iduser select i;
            ViewBag.attendance = attendance;
            return View();
        }
        public IActionResult ClockIn(int id)
        {
            var idUser = _AppDbContext.employees.Find(id);
            var attendance = new Attendance()
            {
                employees = idUser,
                Present = false,
                PresenceIn = DateTime.Now
            };
            _AppDbContext.attendance.Add(attendance);
            _AppDbContext.SaveChanges();
            return RedirectToAction("Clock","EmployeePresence");
        }
        public IActionResult ClockOut(int id)
        {
            var attendance = (from i in _AppDbContext.attendance where i.employees.Id == id select i).OrderBy(z=>z.Id).Last();
            if(attendance.PresenceIn.Date==DateTime.Now.Date)
            {
                var idAttendance = attendance.Id;
                Console.WriteLine(idAttendance);
                var x =_AppDbContext.attendance.Find(idAttendance);
                x.PresenceOut=DateTime.Now;
                x.Present = true;
                _AppDbContext.SaveChanges();

            }
            return RedirectToAction("Clock","EmployeePresence");

        }
        [Authorize]
        public IActionResult LeaveReq()
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;

            var id = from i in _AppDbContext.employees where i.Username == sub select i;
            int idUserLogin =0;
            foreach(var i in id)
            {
                idUserLogin = i.Id;
            }
            var request= from i in _AppDbContext.leaveRequests where i.EmployeeId==idUserLogin select i;
            ViewBag.req=request;
            return View();
        }
        public IActionResult AddLeaveRequest()
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;

            var id = from i in _AppDbContext.employees where i.Username == sub select i;
            int idUserLogin =0;
            foreach(var i in id)
            {
                idUserLogin = i.Id;
            }
            ViewBag.idUserLogin = idUserLogin;
            return View();
        }
        public IActionResult SubmitRequest(int iduser, DateTime leavedate, string reason)
        {
            Console.WriteLine(iduser);
            Console.WriteLine(leavedate);
            Console.WriteLine(reason);
            var emp = _AppDbContext.employees.Find(iduser);
            var x = new LeaveRequest()
            {
                LeaveDate=leavedate,
                Status = "Pending",
                EmployeeId= iduser,
                reason = reason,
                Departement = emp.Departement,
                Phone = emp.Phone,
                Position = emp.Position,
                Email = emp.Email,
                Image = emp.Image,
                Name = emp.Name
            };
            _AppDbContext.leaveRequests.Add(x);
            _AppDbContext.SaveChanges();
            return RedirectToAction("LeaveReq","EmployeePresence");
        }
        public IActionResult Edit(int id)
        {
            var user = _AppDbContext.leaveRequests.Find(id);
            if(user.Status=="Pending")
            {
                ViewBag.user= user;
                return View();
            }
            else
            {
                return RedirectToAction("LeaveReq","EmployeePresence");
            }
        }
        public IActionResult SaveRequest(int iduser, DateTime leavedate, string reason)
        {
            var req = _AppDbContext.leaveRequests.Find(iduser);
            var reasondb =req.reason;
            if(String.IsNullOrEmpty(reason) || String.IsNullOrWhiteSpace(reason))
            {
                req.LeaveDate = leavedate;
                req.reason = reasondb;
                _AppDbContext.SaveChanges();

            }
            else
            {
                req.LeaveDate = leavedate;
                req.reason = reason;
                _AppDbContext.SaveChanges();
            }
            return RedirectToAction("LeaveReq","EmployeePresence");
        }
        
    }
}