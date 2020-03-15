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
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public HomeController(ILogger<HomeController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        public IActionResult IndexForgotPassword()
        {
            return View();
        }
        public IActionResult ChangePassword(string username, string password)
        {
            Console.WriteLine(password);
            if(username!=null)
            {
            var x = (from i in _AppDbContext.employees where i.Username==username select i.Id).First();
            var userId = _AppDbContext.employees.Find(x);
            if(userId!=null)
            {
                userId.Username = username;
                userId.Password = password;
                _AppDbContext.SaveChanges();
                return RedirectToAction("Index","Home");
            }
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Index()
        { 
            return View();
        }
        public IActionResult IndexAdmin(string username, string password)
        {
            IActionResult response = Unauthorized();
            var cek = from x in _AppDbContext.user select x;
            foreach (var item in cek) {
                if (item.Username == username && item.Password == password) {
                    var user = AuthenticatedUser (username, password);
                    if (user != null) {
                        var token = GenerateJwtToken (user);
                        HttpContext.Session.SetString ("JWTToken", token);
                        var get = HttpContext.Session.GetString ("JWTToken");
                    }
                    return RedirectToAction("DashboardIndex","Home");
                }
            }
            return RedirectToAction ("Index","Home");
        }
        public IActionResult IndexEmp(string username, string password)
        {
            System.Console.WriteLine(username);
            System.Console.WriteLine(password);
            IActionResult response = Unauthorized();
            var cek = from x in _AppDbContext.employees select x;
            foreach (var item in cek) {
                if (item.Username == username && item.Password == password) {
                    System.Console.WriteLine("oke");
                    var user = AuthenticatedEmp (username, password);
                    if (user != null) {
                        System.Console.WriteLine("okee");
                        var token = GenerateJwtToken (user);
                        HttpContext.Session.SetString ("JWTToken", token);
                        var get = HttpContext.Session.GetString ("JWTToken");
                    }
                    return RedirectToAction("EmpPresence","Home");
                }
            }     
            return RedirectToAction ("Index","Home");
        }
        private string GenerateJwtToken(User user)
        {
            var securityKey = new SymmetricSecurityKey (Encoding.UTF8.GetBytes (Configuration["Jwt:Key"]));
            var credentials = new SigningCredentials (securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new [] {
                new Claim (JwtRegisteredClaimNames.Sub, Convert.ToString (user.Username)),
                new Claim (JwtRegisteredClaimNames.Jti, Guid.NewGuid ().ToString ())
            };

            var token = new JwtSecurityToken (
                issuer: Configuration["Jwt:Issuer"],
                audience : Configuration["Jwt:Audience"],
                claims,
                expires : DateTime.Now.AddMinutes (2000),
                signingCredentials : credentials);
            var encodedToken = new JwtSecurityTokenHandler ().WriteToken (token);
            return encodedToken;
        }
        private User AuthenticatedUser(string username, string password)
        {
            User user = null;
            user = new User {
                Username = username,
                Password = password,
            };
            return user;
        }
        private User AuthenticatedEmp(string username, string password)
        {
            User user = null;
            user = new User {
                Username = username,
                Password = password,
            };
            return user;
        }

        [Authorize]
        public IActionResult Home()
        {
            return RedirectToAction("ListEmployees","Employees");
        }
        [Authorize]
        public IActionResult ApplicantsIndex()
        {
            return RedirectToAction("ListApplicants","Applicants");
        }
        [Authorize]
        public IActionResult EmpPresence()
        {
            System.Console.WriteLine("masuk");
            return RedirectToAction("Clock","EmployeePresence");
        }

        [Authorize]
        public IActionResult AttendanceIndex()
        {
            return RedirectToAction("Index","Attendance");
        }
        [Authorize]
        public IActionResult LeaveReqIndex()
        {
            return RedirectToAction("ListLeaveRequest","LeaveRequest");
        }
        [Authorize]
        public IActionResult BroadcastIndex()
        {
            return RedirectToAction("Send","Broadcast");
        }
        [Authorize]
        public IActionResult DashboardIndex()
        {
            Console.WriteLine("DASHBOARD");
            return RedirectToAction("HomeDashboard","Dashboard");
        }
        
        
        [Authorize]
        public IActionResult Logout () {
            HttpContext.Session.Remove ("JWTToken");
            return RedirectToAction ("Index","Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
