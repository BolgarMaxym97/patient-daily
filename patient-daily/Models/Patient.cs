using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace patient_daily.Models
{
    [Table("patient")]
    public class Patient
    {
        public int id { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string login { get; set; }
        public string password { get; set; }
        public string address { get; set; }
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        [ForeignKey("Hospital")]
        public int? hospital_id { get; set; }
        public Hospital Hospital { get; set; }

        public ICollection<PatientInfo> PatientInfo { get; set; }

        public Patient()
        {
            PatientInfo = new List<PatientInfo>();
        }

    }
}