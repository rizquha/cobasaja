using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_App.Models
{
    public class Attendance
    {
        public int Id {get;set;}
        public DateTime PresenceIn {get;set;}
        public DateTime PresenceOut {get;set;}
        public bool Present {get;set;}
        [ForeignKey("EmployeesID")]
        public Employees employees {get;set;}



    }
}