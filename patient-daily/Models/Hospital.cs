using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace patient_daily.Models
{
    [Table("hospital")]
    public class Hospital
    {
        public int id { get; set; }
        public string hospital_name { get; set; }
        public string main_doctor { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public int? status { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        public ICollection<Patient> Patients { get; set; }

        public Hospital()
        {
            Patients = new List<Patient>();
        }
    }
}