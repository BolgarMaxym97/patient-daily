using patient_daily.Helpers;
using patient_daily.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;

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
            // TODO: need to be finished
            var test = Crypter.VerifyHashedPassword("AIE0MqrWE4105ddj/NHU0SA1mTYiLHwIqz4yVyjxlMJJy61/s/YwYm5kLvQ3g5CybA==", "newTest6");
            string login = c.Request.Form["login"];
            string password = c.Request.Form["password"];
            bool isHospital = c.Request.Form["isHospital"] == "1";
            if (isHospital)
            {
                return Ok(HospitalFind(login, password));
            }
            return Ok(PatientFind(login, password));
        }

        // POST: api/v1/auth/register-hospital
        [HttpPost]
        [Route("api/v1/auth/register-hospital")]
        public IHttpActionResult PostRegisterHospital(Hospital hospital)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            if (HospitalFindByLogin(hospital.login))
            {
                return BadRequest("This login already exist");
            }
            db.Hospitals.Add(hospital);
            try
            {
                db.SaveChanges();
            }
            catch (System.Exception err)
            {
                return BadRequest(err.ToString());
            }
            return Ok(hospital);
        }

        // POST: api/v1/auth/register-hospital
        [HttpPost]
        [Route("api/v1/auth/register-patient")]
        public IHttpActionResult PostRegisterPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (PatientFindByLogin(patient.login))
            {
                return BadRequest("This login already exist");
            }
            db.Patients.Add(patient);
            try
            {
                db.SaveChanges();
            }
            catch (System.Exception)
            {
                return BadRequest(ModelState);
            }
            return Ok(patient);
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
            return db.Hospitals.Where(p => p.login == login).Include(h => h.Patients);
        }

        private bool PatientFindByLogin(string login)
        {
            return db.Patients.Count(p => p.login == login) > 0;
        }

        private bool HospitalFindByLogin(string login)
        {
            return db.Hospitals.Count(p => p.login == login) > 0;
        }

    }
}
