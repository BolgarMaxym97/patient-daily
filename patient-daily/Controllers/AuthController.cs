using patient_daily.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace patient_daily.Controllers
{
    public class AuthController : ApiController
    {
        private MedicineContext db = new MedicineContext();
        // POST: api/v1/auth
        [HttpPost]
        public IHttpActionResult PostAuth()
        {
            var c = HttpContext.Current;
            string login = c.Request.Form["login"];
            string password = c.Request.Form["password"];
            bool isHospital = c.Request.Form["isHospital"] == "1";
            if (isHospital)
            {
                return Ok(HospitalFind(login, password));
            }
            return Ok(PatientFind(login, password));
        }

        protected override void Dispose(bool disposing)
        {

            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private IEnumerable<Patient> PatientFind(string login, string password)
        {
            return db.Patients.Where(p => p.login == login).Where(p => p.password == password).Include(p => p.Hospital);
        }

        private IEnumerable<Hospital> HospitalFind(string login, string password)
        {
            return db.Hospitals.Where(p => p.login == login).Where(p => p.password == password).Include(h => h.Patients);
        }

    }
}
