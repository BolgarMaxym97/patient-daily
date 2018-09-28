using patient_daily.Models;
using System;
using System.Collections.Generic;
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
        // POST: api/Patients
        [HttpPost]
        public IHttpActionResult PostAuth()
        {
            var c = HttpContext.Current;
            string login = c.Request.Form["login"];
            return Ok(login);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.id == id) > 0;
        }

        private bool HospitaltExists(int id)
        {
            return db.Patients.Count(e => e.id == id) > 0;
        }
    }
}
