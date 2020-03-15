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

namespace HR_App.Controllers
{
    public class ApplicantFormController : Controller
    {
        private readonly ILogger<ApplicantFormController> _logger;
        private AppDBContext _AppDbContext;
        public IConfiguration Configuration;

        public ApplicantFormController(ILogger<ApplicantFormController> logger, AppDBContext appDbContext, IConfiguration configuration)
        {
            _logger = logger;
            _AppDbContext = appDbContext;
            Configuration = configuration;
        }
        public IActionResult Form()
        {
            return View();
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
                Status ="Unprocessed"
            };
            _AppDbContext.applicants.Add(newEmployee);
            _AppDbContext.SaveChanges();
            return RedirectToAction("Thanks","ApplicantForm");
        }
        public IActionResult Thanks()
        {
            return View();
        }
    }
}