using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using patient_daily.Models;

namespace patient_daily.Controllers
{
    public class PatientsController : ApiController
    {
        private MedicineContext db = new MedicineContext();

        // GET: api/patients
        public IQueryable<Patient> GetPatients()
        {
            return db.Patients.Include(p => p.Hospital);
        }

        // GET: api/patient/{id}
        [ResponseType(typeof(Patient))]
        public IHttpActionResult GetPatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // GET: api/Patients/5
        [ResponseType(typeof(PatientInfo))]
        [HttpGet]
        [Route("api/v1/patient-info/{id}")]
        public IHttpActionResult GetPatientInfo(int id)
        {
            try
            {
                var info = db.PatientInfo.Where(i => i.patient_id == id);
                if (info == null)
                {
                    return NotFound();
                }

                return Ok(info);
            }
            catch (Exception)
            {
                return NotFound();
            }
           
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatient(int id, Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.id)
            {
                return BadRequest();
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        public IHttpActionResult PostPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Patients.Add(patient);
            db.SaveChanges();

            return Ok(patient);
        }

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        [HttpPost]
        [Route("api/v1/patient-reject")]
        public IHttpActionResult PostPatientReject(int id)
        {
            Patient patient = db.Patients.Find(id);
            patient.hospital_id = null;
            patient.Hospital = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(patient).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(ModelState);
            }

            return Ok(patient);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        public IHttpActionResult DeletePatient(int id)
        {
            Patient patient = db.Patients.Find(id);
            if (patient == null)
            {
                return NotFound();
            }

            db.Patients.Remove(patient);
            db.SaveChanges();

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

        private bool PatientExists(int id)
        {
            return db.Patients.Count(e => e.id == id) > 0;
        }
    }
}