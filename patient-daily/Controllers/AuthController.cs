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
            hospital.password = Crypter.HashPassword(hospital.password);
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
            patient.password = Crypter.HashPassword(patient.password);
            patient.Hospital = db.Hospitals.Where(h => h.id == patient.hospital_id).First();
            db.Patients.Add(patient);
            try
            {
                db.SaveChanges();
            }
            catch (System.Exception err)
            {
                return BadRequest(err.ToString());
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

        private object PatientFind(string login, string password)
        {
            try
            {
                Patient patient = db.Patients.Where(p => p.login == login).Include(p => p.Hospital).Single();
                if (!Crypter.VerifyHashedPassword(patient.password, password))
                {
                    return new object();
                }
                return patient;

            }
            catch (System.Exception)
            { 
                return new object();
            }
        }

        private object HospitalFind(string login, string password)
        {
            try
            {
                Hospital hospital = db.Hospitals.Where(p => p.login == login).Include(h => h.Patients).Single();
                if (!Crypter.VerifyHashedPassword(hospital.password, password))
                {
                    return new object();
                }
                return hospital;
            }
            catch (System.Exception)
            {
                return new object();
            }
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
