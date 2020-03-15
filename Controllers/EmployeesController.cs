using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using HR_App.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using MimeKit;

namespace HR_App.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly ILogger<EmployeesController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public EmployeesController(ILogger<EmployeesController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        public IActionResult ListEmployees(string search, string val,int? page, int PerPage)
        {
            Console.WriteLine("LIST");
            Console.WriteLine(val);
            if(!String.IsNullOrEmpty(val) || !String.IsNullOrWhiteSpace(val))
            {
                ViewBag.Message = val;
                ViewBag.search = search;
            }
            else
            {
                val="Home";
            }
            Console.WriteLine(val);

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
                    System.Console.WriteLine("home");
                    var employees = from i in _AppDbContext.employees where i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search) select i;
                    ViewBag.employees = employees;
                    var employeesPermanent = from i in _AppDbContext.employees where i.Status=="Permanent" select i;
                    ViewBag.employeesPermanent = employeesPermanent;
                    var employeesContract = from i in _AppDbContext.employees where i.Status=="Contract" select i;
                    ViewBag.employeesContract = employeesContract;
                    var employeesProbation = from i in _AppDbContext.employees where i.Status=="Probation" select i;
                    ViewBag.employeesProbation = employeesProbation;

                    var pager = new Pager(employees.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Employee = employees.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="News")
                {
                    System.Console.WriteLine("news");
                    var employeesPermanent = from i in _AppDbContext.employees where i.Status=="Permanent" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.employeesPermanent = employeesPermanent;
                    var employees = from i in _AppDbContext.employees select i;
                    ViewBag.employees = employees;
                    var employeesContract = from i in _AppDbContext.employees where i.Status=="Contract" select i;
                    ViewBag.employeesContract = employeesContract;
                    var employeesProbation = from i in _AppDbContext.employees where i.Status=="Probation" select i;
                    ViewBag.employeesProbation = employeesProbation;

                    var pager = new Pager(employeesPermanent.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Employee = employeesPermanent.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="Contact")
                {
                    System.Console.WriteLine("contact");
                    var employeesContract = from i in _AppDbContext.employees where i.Status=="Contract" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.employeesContract = employeesContract;
                    var employees = from i in _AppDbContext.employees select i;
                    ViewBag.employees = employees;
                    var employeesPermanent = from i in _AppDbContext.employees where i.Status=="Permanent" select i;
                    ViewBag.employeesPermanent = employeesPermanent;
                    var employeesProbation = from i in _AppDbContext.employees where i.Status=="Probation" select i;
                    ViewBag.employeesProbation = employeesProbation;

                    var pager = new Pager(employeesContract.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Employee = employeesContract.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);

                }
                else if(val=="About")
                {
                    System.Console.WriteLine("about");
                    var employeesProbation = from i in _AppDbContext.employees where i.Status=="Probation" && (i.Name.Contains(search) || i.Email.Contains(search) || i.Phone.Contains(search) || i.Position.Contains(search) || i.Departement.Contains(search)) select i;
                    ViewBag.employeesProbation = employeesProbation; 
                    var employees = from i in _AppDbContext.employees select i;
                    ViewBag.employees = employees;
                    var employeesPermanent = from i in _AppDbContext.employees where i.Status=="Permanent" select i;
                    ViewBag.employeesPermanent = employeesPermanent;
                    var employeesContract = from i in _AppDbContext.employees where i.Status=="Contract" select i;
                    ViewBag.employeesContract = employeesContract;

                    var pager = new Pager(employeesProbation.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Employee = employeesProbation.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
            }else{
                var employees = from i in _AppDbContext.employees select i;
                ViewBag.employees = employees;
                var employeesPermanent = from i in _AppDbContext.employees where i.Status=="Permanent" select i;
                ViewBag.employeesPermanent = employeesPermanent;
                var employeesContract = from i in _AppDbContext.employees where i.Status=="Contract" select i;
                ViewBag.employeesContract = employeesContract;
                var employeesProbation = from i in _AppDbContext.employees where i.Status=="Probation" select i;
                ViewBag.employeesProbation = employeesProbation;
                    var pager = new Pager(employees.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Employee = employees.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
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
            var details = from i in _AppDbContext.employees where i.Id==id select i;
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
                status="Permanent";
            }else if(val=="Contact")
            {
                status="Contract";
            }else if(val=="About")
            {
                status="Probation";
            }
            Console.WriteLine("STATUS");
            Console.WriteLine(status);

            var comlumHeadrs = new string[]
            {
                "Id",
                "Name",
                "Email",
                "Phone",
                "Gender",
                "BirthDate",
                "BirthPlace",
                "Position",
                "Departement",
                "Address",
                "Status",
                "CreatedAt",
                "Contact Name Emergency1",
                "Contact Phone Emergency1",
                "Contact Name Emergency2",
                "Contact Phone Emergency2",
                "Contact Name Emergency3",
                "Contact Phone Emergency3"
            };
            var items= new List<object[]>();
            if(val=="Home")
            {
                if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
                {
                    items = (from item in _AppDbContext.employees where item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search)
                            select new object[]
                            {
                                        item.Id,
                                        $"{item.Name}",
                                        $"{item.Email}",
                                        $"{item.Phone}",
                                        $"{item.Gender}",
                                        $"{item.BirthDate}",
                                        $"{item.BirthPlace}",
                                        $"{item.Position}",
                                        $"{item.Departement}",
                                        $"{item.Address}",
                                        $"{item.Status}",
                                        $"{item.CreatedAt}",
                                        $"{item.NameEmergencyContact1}",
                                        $"{item.PhoneEmergencyContact1}",
                                        $"{item.NameEmergencyContact2}",
                                        $"{item.PhoneEmergencyContact2}",
                                        $"{item.NameEmergencyContact3}",
                                        $"{item.PhoneEmergencyContact3}",
                            }).ToList();
                }
                else
                {
                    items = (from item in _AppDbContext.employees
                         select new object[]
                         {
                                       item.Id,
                                       $"{item.Name}",
                                       $"{item.Email}",
                                       $"{item.Phone}",
                                       $"{item.Gender}",
                                       $"{item.BirthDate}",
                                       $"{item.BirthPlace}",
                                       $"{item.Position}",
                                       $"{item.Departement}",
                                       $"{item.Address}",
                                       $"{item.Status}",
                                       $"{item.CreatedAt}",
                                       $"{item.NameEmergencyContact1}",
                                       $"{item.PhoneEmergencyContact1}",
                                       $"{item.NameEmergencyContact2}",
                                       $"{item.PhoneEmergencyContact2}",
                                       $"{item.NameEmergencyContact3}",
                                       $"{item.PhoneEmergencyContact3}",
                         }).ToList();
                }
            }else
            {
                Console.WriteLine("NOTHOME");
                if(!String.IsNullOrEmpty(search) || !String.IsNullOrWhiteSpace(search))
                {
                    Console.WriteLine("NOT HOME SEARCH NOT");
                    items = (from item in _AppDbContext.employees where item.Status==status && (item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search))
                         select new object[]
                         {
                                       item.Id,
                                       $"{item.Name}",
                                       $"{item.Email}",
                                       $"{item.Phone}",
                                       $"{item.Gender}",
                                       $"{item.BirthDate}",
                                       $"{item.BirthPlace}",
                                       $"{item.Position}",
                                       $"{item.Departement}",
                                       $"{item.Address}",
                                       $"{item.Status}",
                                       $"{item.CreatedAt}",
                                       $"{item.NameEmergencyContact1}",
                                       $"{item.PhoneEmergencyContact1}",
                                       $"{item.NameEmergencyContact2}",
                                       $"{item.PhoneEmergencyContact2}",
                                       $"{item.NameEmergencyContact3}",
                                       $"{item.PhoneEmergencyContact3}",
                         }).ToList();
                }else
                {
                    Console.WriteLine("NOT HOME SEARCH");
                    items = (from item in _AppDbContext.employees where item.Status==status
                         select new object[]
                         {
                                       item.Id,
                                       $"{item.Name}",
                                       $"{item.Email}",
                                       $"{item.Phone}",
                                       $"{item.Gender}",
                                       $"{item.BirthDate}",
                                       $"{item.BirthPlace}",
                                       $"{item.Position}",
                                       $"{item.Departement}",
                                       $"{item.Address}",
                                       $"{item.Status}",
                                       $"{item.CreatedAt}",
                                       $"{item.NameEmergencyContact1}",
                                       $"{item.PhoneEmergencyContact1}",
                                       $"{item.NameEmergencyContact2}",
                                       $"{item.PhoneEmergencyContact2}",
                                       $"{item.NameEmergencyContact3}",
                                       $"{item.PhoneEmergencyContact3}",
                         }).ToList();
                }
            }

            var itemcsv = new StringBuilder();
            items.ForEach(line =>
                {
                    itemcsv.AppendLine(string.Join(",", line));
                });

            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{itemcsv.ToString()}");
            return File(buffer, "text/csv", $"AllEmployees.csv");
        }
        public IActionResult Export()
        {
            var comlumHeadrs = new string[]
            {
                "Id",
                "Name",
                "Email",
                "Phone",
                "Gender",
                "BirthDate",
                "BirthPlace",
                "Position",
                "Departement",
                "Address",
                "Status",
                "CreatedAt",
                "Contact Name Emergency1",
                "Contact Phone Emergency1",
                "Contact Name Emergency2",
                "Contact Phone Emergency2",
                "Contact Name Emergency3",
                "Contact Phone Emergency3"
            };
            var items = (from item in _AppDbContext.employees
                         select new object[]
                         {
                                       item.Id,
                                       $"{item.Name}",
                                       $"{item.Email}",
                                       $"{item.Phone}",
                                       $"{item.Gender}",
                                       $"{item.BirthDate}",
                                       $"{item.BirthPlace}",
                                       $"{item.Position}",
                                       $"{item.Departement}",
                                       $"{item.Address}",
                                       $"{item.Status}",
                                       $"{item.CreatedAt}",
                                       $"{item.NameEmergencyContact1}",
                                       $"{item.PhoneEmergencyContact1}",
                                       $"{item.NameEmergencyContact2}",
                                       $"{item.PhoneEmergencyContact2}",
                                       $"{item.NameEmergencyContact3}",
                                       $"{item.PhoneEmergencyContact3}",
                         }).ToList();

            var itemcsv = new StringBuilder();
            items.ForEach(line =>
                {
                    itemcsv.AppendLine(string.Join(",", line));
                });

            byte[] buffer = Encoding.ASCII.GetBytes($"{string.Join(",", comlumHeadrs)}\r\n{itemcsv.ToString()}");
            return File(buffer, "text/csv", $"AllEmployees.csv");
        }
        public IActionResult Import([FromForm(Name = "file")] IFormFile file)
        {
            string filePath = string.Empty;
            if (file != null)
            {
                try
                {
                    string fileExtension = Path.GetExtension(file.FileName);
                    if (fileExtension != ".csv")
                    {
                        
                        ViewBag.Message = "Please select the csv file";
                        return RedirectToAction("ListEmployees", "Employees");
                    }
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        string[] header = reader.ReadLine().Split(',');
                        while (!reader.EndOfStream)
                        {
                            string[] rows = reader.ReadLine().Split(',');
                            var emp = new Employees()
                            {
                                Name = rows[1].ToString(),
                                Email = rows[2].ToString(),
                                Phone = rows[3].ToString(),
                                Gender =rows[4].ToString(),
                                BirthDate =Convert.ToDateTime(rows[5].ToString()),
                                BirthPlace =rows[6].ToString(),
                                Position =rows[7].ToString(),
                                Departement =rows[8].ToString(),
                                Address =rows[9].ToString(),
                                Status =rows[10].ToString(),
                                CreatedAt = Convert.ToDateTime(rows[11].ToString()),
                                NameEmergencyContact1 =rows[12].ToString(),
                                PhoneEmergencyContact1 =rows[13].ToString(),
                                NameEmergencyContact2 =rows[14].ToString(),
                                PhoneEmergencyContact2 =rows[15].ToString(),
                                NameEmergencyContact3 =rows[16].ToString(),
                                PhoneEmergencyContact3 =rows[17].ToString(),
                                Image= rows[18].ToString()
                            };
                            _AppDbContext.employees.Add(emp);
                        }
                        _AppDbContext.SaveChanges();
                    }
                    return RedirectToAction("ListEmployees", "Employees");
                }
                catch (Exception e)
                {
                    ;
                    ViewBag.Message = e.Message;
                }
            }
            return RedirectToAction("ListEmployees", "Employees");
        }
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DELETE");
            Console.WriteLine(id);
            var emp = _AppDbContext.employees.Find(id);
            _AppDbContext.employees.Remove(emp);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListEmployees","Employees");
        }
        [Authorize]
        public IActionResult Edit(int id)
        {
            var token = HttpContext.Session.GetString ("JWTToken");
            var jwtSec = new JwtSecurityTokenHandler ();
            var securityToken = jwtSec.ReadToken (token) as JwtSecurityToken;
            var sub = securityToken?.Claims.First (Claim => Claim.Type == "sub").Value;
            
            ViewBag.admin=sub;
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            var updtemployee = from i in _AppDbContext.employees where i.Id == id select i;
            ViewBag.update = updtemployee;
            return View("Edit","Employees");
        }

        public IActionResult SaveEdit(int Id1, string name1, string email1, string phone1, string radio1, DateTime birthdate1, 
        string birthplace1, string position1, string departement1, IFormFile image1, string address1, string personname11, 
        string personphone11, string personname21, string personphone21, string personname31, string personphone31,string status1)
        {
            if(image1!=null)
            {
            var path ="wwwroot/images";
            Directory.CreateDirectory(path);
            var FileName = Path.Combine(path,Path.GetFileName(image1.FileName));
            image1.CopyTo(new FileStream(FileName,FileMode.Create));
            var file = FileName.Substring(7).Replace(@"\","/");
            var User = _AppDbContext.employees.Find(Id1);
                User.Name=name1;
                User.Email = email1;
                User.Phone= phone1;
                User.Gender= radio1;
                User.BirthDate = birthdate1;
                User.BirthPlace = birthplace1;
                User.Position = position1;
                User.Departement = departement1;
                User.Address = address1;
                User.NameEmergencyContact1= personname11;
                User.NameEmergencyContact2 = personname21;
                User.NameEmergencyContact3 = personname31;
                User.PhoneEmergencyContact1 = personphone11;
                User.PhoneEmergencyContact3 = personphone31;
                User.PhoneEmergencyContact2 = personphone21;
                User.Image = file;
                User.CreatedAt = DateTime.Now;
                User.Status =status1;
            _AppDbContext.SaveChanges();
            }else
            {
                var User = _AppDbContext.employees.Find(Id1);
                User.Name=name1;
                User.Email = email1;
                User.Phone= phone1;
                User.Gender= radio1;
                User.BirthDate = birthdate1;
                User.BirthPlace = birthplace1;
                User.Position = position1;
                User.Departement = departement1;
                User.Address = address1;
                User.NameEmergencyContact1= personname11;
                User.NameEmergencyContact2 = personname21;
                User.NameEmergencyContact3 = personname31;
                User.PhoneEmergencyContact1 = personphone11;
                User.PhoneEmergencyContact3 = personphone31;
                User.PhoneEmergencyContact2 = personphone21;
                User.CreatedAt = DateTime.Now;
                User.Status =status1;
                _AppDbContext.SaveChanges();
            }
            return RedirectToAction("ListEmployees","Employees");
        }
        public IActionResult Save(string name1, string email1, string phone1, string radio1, DateTime birthdate1, 
        string birthplace1, string position1, string departement1, IFormFile image1, string address1, string personname11, 
        string personphone11, string personname21, string personphone21, string personname31, string personphone31,string status1, string username, string password)
        {
            var path ="wwwroot/images";
            Directory.CreateDirectory(path);
            var FileName = Path.Combine(path,Path.GetFileName(image1.FileName));
            image1.CopyTo(new FileStream(FileName,FileMode.Create));
            var file = FileName.Substring(7).Replace(@"\","/");
            var newEmployee = new Employees()
            {
                Name=name1,
                Email = email1,
                Phone= phone1,
                Gender= radio1,
                BirthDate = birthdate1,
                BirthPlace = birthplace1,
                Position = position1,
                Departement = departement1,
                Address = address1,
                NameEmergencyContact1= personname11,
                NameEmergencyContact2 = personname21,
                NameEmergencyContact3 = personname31,
                PhoneEmergencyContact1 = personphone11,
                PhoneEmergencyContact3 = personphone31,
                PhoneEmergencyContact2 = personphone21,
                Image = file,
                CreatedAt = DateTime.Now,
                Status =status1,
                Username = username,
                Password = password
            };
            _AppDbContext.employees.Add(newEmployee);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListEmployees","Employees");
        }
        public IActionResult SaveAndNew(string name1, string email1, string phone1, string radio1, DateTime birthdate1, 
        string birthplace1, string position1, string departement1, IFormFile image1, string address1, string personname11, 
        string personphone11, string personname21, string personphone21, string personname31, string personphone31,string status1, string username, string password)
        {
            var path ="wwwroot/images";
            Directory.CreateDirectory(path);
            var FileName = Path.Combine(path,Path.GetFileName(image1.FileName));
            image1.CopyTo(new FileStream(FileName,FileMode.Create));
            var file = FileName.Substring(7).Replace(@"\","/");
            var newEmployee = new Employees()
            {
                Name=name1,
                Email = email1,
                Phone= phone1,
                Gender= radio1,
                BirthDate = birthdate1,
                BirthPlace = birthplace1,
                Position = position1,
                Departement = departement1,
                Address = address1,
                NameEmergencyContact1= personname11,
                NameEmergencyContact2 = personname21,
                NameEmergencyContact3 = personname31,
                PhoneEmergencyContact1 = personphone11,
                PhoneEmergencyContact3 = personphone31,
                PhoneEmergencyContact2 = personphone21,
                Image = file,
                CreatedAt = DateTime.Now,
                Status =status1,
                Username = username,
                Password = password
            };
            _AppDbContext.employees.Add(newEmployee);
            _AppDbContext.SaveChanges();
            return RedirectToAction("NewEmployee","Employees");
        }
        public IActionResult NewEmployee()
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
        [Authorize]
        public IActionResult Warning(int id)
        {
            var countLeave = from i in _AppDbContext.leaveRequests where i.Status=="Pending" select i;
            var countLeave1=countLeave.Count();
            ViewBag.countLeaveReq=countLeave1;
            ViewBag.iduser = id;
            return View();
        }
        public IActionResult SendWarningMessage(string title, string message, int iduser)
        {
            var msg = new MimeMessage();
            var user = _AppDbContext.employees.Find(iduser);
            string name = user.Name;
            string email= user.Email;
                msg.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                msg.To.Add(new MailboxAddress(name,email));
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
            return RedirectToAction("ListEmployees","Employees");
        }
    }
}