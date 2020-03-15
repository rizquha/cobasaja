using System;

namespace HR_App.Models
{
    public class Applicants
    {
        public int Id {get;set;}
        public string Image {get;set;}
        public string CV {get;set;}
        public string Name {get;set;}
        public string Email {get;set;}
        public string Phone {get;set;}
        public string Gender {get;set;}
        public DateTime BirthDate {get;set;}
        public string BirthPlace {get;set;}
        public DateTime CreatedAt {get;set;}
        public string Position {get;set;}
        public string Departement {get;set;}
        public string Address {get;set;}
        public string Status {get;set;}
        public string NameEmergencyContact1 {get;set;}
        public string PhoneEmergencyContact1 {get;set;}
        public string NameEmergencyContact2 {get;set;}
        public string PhoneEmergencyContact2 {get;set;}
        public string NameEmergencyContact3 {get;set;}
        public string PhoneEmergencyContact3 {get;set;}
    }
}