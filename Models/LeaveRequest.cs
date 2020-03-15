using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HR_App.Models
{
    public class LeaveRequest
    {
        public int Id {get;set;}
        public DateTime LeaveDate {get;set;}
        public string reason {get;set;}
        public string Status {get;set;}
        public int EmployeeId {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public string Phone {get;set;}
        public string Position {get;set;}
        public string Departement {get;set;}
        public string Image {get;set;}

    }
}