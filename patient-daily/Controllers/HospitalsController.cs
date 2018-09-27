using patient_daily.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace patient_daily.Controllers
{
    public class HospitalsController : ApiController
    {
        MedicineContext db = new MedicineContext();
        // GET api/hospitals
        public IEnumerable<Hospital> Get()
        {
            var hospitals = db.Hospitals.Include(h => h.Patients);
            return hospitals.ToArray();
        }
    }
}
