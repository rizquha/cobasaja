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
    public class ApplicantsController : Controller
    {
        private readonly ILogger<ApplicantsController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public ApplicantsController(ILogger<ApplicantsController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        [Authorize]
        public IActionResult ListApplicants(string search, string val,int? page, int PerPage)
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
                    System.Console.WriteLine("home");
                    var applicants = from x in _AppDbContext.applicants where (x.Name.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search) || x.Position.Contains(search) || x.Departement.Contains(search)) select x;
                    ViewBag.applicants = applicants;
                    var applicantsUnprocessed = from x in _AppDbContext.applicants where x.Status=="Unprocessed" select x;
                    ViewBag.applicantsUnprocessed = applicantsUnprocessed;
                    var applicantsScheduledPsychoTest = from x in _AppDbContext.applicants where x.Status=="Scheduled to Psycho Test" select x;
                    ViewBag.applicantsScheduledPsychoTest = applicantsScheduledPsychoTest;
                    var applicantsScheduledInterview = from x in _AppDbContext.applicants where x.Status=="Scheduled to Interview" select x;
                    ViewBag.applicantsScheduledInterview = applicantsScheduledInterview;
                    var pager = new Pager(applicants.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Applicant = applicants.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="News")
                {
                    System.Console.WriteLine("news");
                    var applicants = from x in _AppDbContext.applicants select x;
                    ViewBag.applicants = applicants;
                    var applicantsUnprocessed = from x in _AppDbContext.applicants where x.Status=="Unprocessed" && (x.Name.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search) || x.Position.Contains(search) || x.Departement.Contains(search)) select x;
                    ViewBag.applicantsUnprocessed = applicantsUnprocessed;
                    var applicantsScheduledPsychoTest = from x in _AppDbContext.applicants where x.Status=="Scheduled to Psycho Test" select x;
                    ViewBag.applicantsScheduledPsychoTest = applicantsScheduledPsychoTest;
                    var applicantsScheduledInterview = from x in _AppDbContext.applicants where x.Status=="Scheduled to Interview" select x;
                    ViewBag.applicantsScheduledInterview = applicantsScheduledInterview;
                    var pager = new Pager(applicantsUnprocessed.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Applicant = applicantsUnprocessed.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
                else if(val=="Contact")
                {
                    System.Console.WriteLine("contact");
                    var applicants = from x in _AppDbContext.applicants select x;
                    ViewBag.applicants = applicants;
                    var applicantsUnprocessed = from x in _AppDbContext.applicants where x.Status=="Unprocessed" select x;
                    ViewBag.applicantsUnprocessed = applicantsUnprocessed;
                    var applicantsScheduledPsychoTest = from x in _AppDbContext.applicants where x.Status=="Scheduled to Psycho Test" && (x.Name.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search) || x.Position.Contains(search) || x.Departement.Contains(search)) select x;
                    ViewBag.applicantsScheduledPsychoTest = applicantsScheduledPsychoTest;
                    var applicantsScheduledInterview = from x in _AppDbContext.applicants where x.Status=="Scheduled to Interview" select x;
                    ViewBag.applicantsScheduledInterview = applicantsScheduledInterview;
                    var pager = new Pager(applicantsScheduledPsychoTest.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Applicant = applicantsScheduledPsychoTest.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                   
                }
                else if(val=="About")
                {
                    System.Console.WriteLine("about");
                    var applicants = from x in _AppDbContext.applicants select x;
                    ViewBag.applicants = applicants;
                    var applicantsUnprocessed = from x in _AppDbContext.applicants where x.Status=="Unprocessed" select x;
                    ViewBag.applicantsUnprocessed = applicantsUnprocessed;
                    var applicantsScheduledPsychoTest = from x in _AppDbContext.applicants where x.Status=="Scheduled to Psycho Test" select x;
                    ViewBag.applicantsScheduledPsychoTest = applicantsScheduledPsychoTest;
                    var applicantsScheduledInterview = from x in _AppDbContext.applicants where x.Status=="Scheduled to Interview" && (x.Name.Contains(search) || x.Email.Contains(search) || x.Phone.Contains(search) || x.Position.Contains(search) || x.Departement.Contains(search)) select x;
                    ViewBag.applicantsScheduledInterview = applicantsScheduledInterview;
                    var pager = new Pager(applicantsScheduledInterview.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Applicant = applicantsScheduledInterview.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
                        Pager = pager
                    };
                    return View(viewModel);
                }
            }else{
                var applicants = from i in _AppDbContext.applicants select i;
                ViewBag.applicants = applicants;
                var applicantsUnprocessed = from i in _AppDbContext.applicants where i.Status=="Unprocessed" select i;
                ViewBag.applicantsUnprocessed = applicantsUnprocessed;
                var applicantsScheduledPsychoTest = from i in _AppDbContext.applicants where i.Status=="Scheduled to Psycho Test" select i;
                ViewBag.applicantsScheduledPsychoTest = applicantsScheduledPsychoTest;
                var applicantsScheduledInterview = from i in _AppDbContext.applicants where i.Status=="Scheduled to Interview" select i;
                ViewBag.applicantsScheduledInterview = applicantsScheduledInterview;
                var pager = new Pager(applicants.Count(), page, PerPage);

                    var viewModel = new IndexViewModel
                    {
                        Applicant = applicants.Skip((pager.CurrentPage - 1) * pager.PageSize).Take(pager.PageSize),
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
            var details = from i in _AppDbContext.applicants where i.Id==id select i;
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
                status="Unprocessed";
            }else if(val=="Contact")
            {
                status="Scheduled to Psycho Test";
            }else if(val=="About")
            {
                status="Scheduled to Interview";
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
                    items = (from item in _AppDbContext.applicants where item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search)
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
                    items = (from item in _AppDbContext.applicants
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
                    items = (from item in _AppDbContext.applicants where item.Status==status && (item.Name.Contains(search) || item.Email.Contains(search) || item.Phone.Contains(search) || item.Position.Contains(search) || item.Departement.Contains(search))
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
                    items = (from item in _AppDbContext.applicants where item.Status==status
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
            return File(buffer, "text/csv", $"AllApplicants.csv");

            
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
            var items = (from item in _AppDbContext.applicants
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
            return File(buffer, "text/csv", $"AllAplicants.csv");
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
                        return RedirectToAction("ListApplicants","Applicants");
                    }
                    using (var reader = new StreamReader(file.OpenReadStream()))
                    {
                        string[] header = reader.ReadLine().Split(',');
                        while (!reader.EndOfStream)
                        {
                            string[] rows = reader.ReadLine().Split(',');
                            Console.WriteLine(rows[1]);
                            Console.WriteLine(rows[2]);
                            Console.WriteLine(rows[3]);
                            Console.WriteLine(rows[4]);
                            Console.WriteLine(rows[5]);
                            Console.WriteLine(rows[6]);
                            Console.WriteLine(rows[7]);
                            Console.WriteLine(rows[8]);
                            Console.WriteLine(rows[9]);
                            Console.WriteLine(rows[10]);
                            Console.WriteLine(rows[11]);
                            Console.WriteLine(rows[12]);
                            Console.WriteLine(rows[13]);
                            Console.WriteLine(rows[14]);
                            Console.WriteLine(rows[15]);
                            Console.WriteLine(rows[16]);
                            Console.WriteLine(rows[17]);
                            Console.WriteLine(rows[18]);
                            var emp = new Applicants()
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
                            _AppDbContext.applicants.Add(emp);
                        }
                        _AppDbContext.SaveChanges();
                    }
                    return RedirectToAction("ListApplicants","Applicants");
                }
                catch (Exception e)
                {
                    ;
                    ViewBag.Message = e.Message;
                }
            }
            return RedirectToAction("ListApplicants","Applicants");
        }
        public IActionResult Delete(int id)
        {
            Console.WriteLine("DELETE");
            Console.WriteLine(id);
            var emp = _AppDbContext.applicants.Find(id);
            _AppDbContext.applicants.Remove(emp);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListApplicants","Applicants");
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
            var updtapplicants = from i in _AppDbContext.applicants where i.Id == id select i;
            ViewBag.update = updtapplicants;
            return View("Edit","Applicants");
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
            var User = _AppDbContext.applicants.Find(Id1);
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
                var User = _AppDbContext.applicants.Find(Id1);
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
            return RedirectToAction("ListApplicants","Applicants");
        }
        public IActionResult Save(string name1, string email1, string phone1, string radio1, DateTime birthdate1, 
        string birthplace1, string position1, string departement1, IFormFile image1, IFormFile cv1 ,string address1, string personname11, 
        string personphone11, string personname21, string personphone21, string personname31, string personphone31,string status1)
        {
            var CVpath ="wwwroot/CV";
            Directory.CreateDirectory(CVpath);
            var FileNameCV = Path.Combine(CVpath,Path.GetFileName(cv1.FileName));
            cv1.CopyTo(new FileStream(FileNameCV,FileMode.Create));
            var fileCV = FileNameCV.Substring(7).Replace(@"\","/");

            var path ="wwwroot/images";
            Directory.CreateDirectory(path);
            var FileName = Path.Combine(path,Path.GetFileName(image1.FileName));
            image1.CopyTo(new FileStream(FileName,FileMode.Create));
            var file = FileName.Substring(7).Replace(@"\","/");
            var newEmployee = new Applicants()
            {
                CV= fileCV,
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
                Status =status1
            };
            _AppDbContext.applicants.Add(newEmployee);
            _AppDbContext.SaveChanges();
            return RedirectToAction("ListApplicants","Applicants");
        }
        public IActionResult SaveAndNew(string name1, string email1, string phone1, string radio1, DateTime birthdate1, 
        string birthplace1, string position1, string departement1, IFormFile image1, string address1, string personname11, 
        string personphone11, string personname21, string personphone21, string personname31, string personphone31,string status1)
        {
            var path ="wwwroot/images";
            Directory.CreateDirectory(path);
            var FileName = Path.Combine(path,Path.GetFileName(image1.FileName));
            image1.CopyTo(new FileStream(FileName,FileMode.Create));
            var file = FileName.Substring(7).Replace(@"\","/");
            var newEmployee = new Applicants()
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
                Status =status1
            };
            _AppDbContext.applicants.Add(newEmployee);
            _AppDbContext.SaveChanges();
            return RedirectToAction("NewApplicant","Applicants");
        }
        public IActionResult NewApplicant()
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
        public IActionResult Process(int id)
        {
            var idUser = _AppDbContext.applicants.Find(id);
            string nama = idUser.Name;
            string email = idUser.Email;
            DateTime created = idUser.CreatedAt;
            if(idUser.Status=="Unprocessed")
            {
                string status ="Scheduled to Psycho Test";
                idUser.Status="Scheduled to Psycho Test";
                SendEmail(nama,email,created,status);
                _AppDbContext.SaveChanges();
            }else if(idUser.Status =="Scheduled to Psycho Test")
            {
                string status ="Scheduled to Interview";
                idUser.Status ="Scheduled to Interview";
                SendEmail(nama,email,created,status);
                _AppDbContext.SaveChanges();
            }else if(idUser.Status=="Scheduled to Interview")
            {
                string status ="Hired";
                idUser.Status ="Hired";
                SendEmail(nama,email,created,status);
                var employee = new Employees()
                {
                    Name = idUser.Name,
                    Email = idUser.Email,
                    Phone = idUser.Phone,
                    Gender = idUser.Gender,
                    BirthDate = idUser.BirthDate,
                    BirthPlace = idUser.BirthPlace,
                    CreatedAt = DateTime.Now,
                    Position = idUser.Position,
                    Departement = idUser.Departement,
                    Address = idUser.Address,
                    Status = "Probation",
                    NameEmergencyContact1= idUser.NameEmergencyContact1,
                    NameEmergencyContact2 = idUser.NameEmergencyContact2,
                    NameEmergencyContact3 = idUser.NameEmergencyContact3,
                    PhoneEmergencyContact1 = idUser.PhoneEmergencyContact1,
                    PhoneEmergencyContact2 = idUser.PhoneEmergencyContact2,
                    PhoneEmergencyContact3 = idUser.PhoneEmergencyContact3
                };
                _AppDbContext.employees.Add(employee);
                _AppDbContext.SaveChanges();
            }
            return RedirectToAction("ListApplicants","Applicants");
        }
        private void SendEmail(string nama, string email, DateTime created, string status)
        {
                var msg = new MimeMessage();
                msg.From.Add(new MailboxAddress("HR","HR@bsi.co.id"));
                msg.To.Add(new MailboxAddress(nama,email));
                msg.Subject ="Update Your Application Request";
                msg.Body = new TextPart("Plain")
                {
                    Text = "Hai "+nama+",\n"+"Selamat Status Application yang Anda Kirim Pada Tanggal"+created.Date+".\n"+"Telah Berubah Status Menjadi "+status
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
    }
}