using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace patient_daily.Models
{
    [Table("patients_info")]
    public class PatientInfo
    {
        public int id { get; set; }
        public string name { get; set; }
        public string text { get; set; }
        public DateTime? date { get; set; } = DateTime.Now;
        public DateTime? created_at { get; set; } = DateTime.Now;
        public DateTime? updated_at { get; set; } = DateTime.Now;
        [ForeignKey("Patient")]
        public int? patient_id { get; set; }

        public Patient Patient { get; set; }
    }
}