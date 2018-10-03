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
    public class PatientInfoController : ApiController
    {
        private MedicineContext db = new MedicineContext();

        // GET: api/PatientInfo
        public IQueryable<PatientInfo> GetPatientInfoes()
        {
            return db.PatientInfo;
        }

        // GET: api/PatientInfo/5
        [ResponseType(typeof(PatientInfo))]
        public IHttpActionResult GetPatientInfo(int id)
        {
            PatientInfo patientInfo = db.PatientInfo.Find(id);
            if (patientInfo == null)
            {
                return NotFound();
            }

            return Ok(patientInfo);
        }

        // PUT: api/PatientInfo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPatientInfo(int id, PatientInfo patientInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patientInfo.id)
            {
                return BadRequest();
            }

            db.Entry(patientInfo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientInfoExists(id))
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

        // POST: api/v1/PatientInfo
        [ResponseType(typeof(PatientInfo))]
        [Route("api/v1/auth/patient-info/create")]
        public IHttpActionResult PostPatientInfoCreate(PatientInfo patientInfo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                db.PatientInfo.Add(patientInfo);
                db.SaveChanges();
            }
            catch (System.Exception err)
            {
                return BadRequest(err.ToString());
            }

            return Ok(patientInfo);
        }

        // DELETE: api/PatientInfo/5
        [ResponseType(typeof(PatientInfo))]
        public IHttpActionResult DeletePatientInfo(int id)
        {
            PatientInfo patientInfo = db.PatientInfo.Find(id);
            if (patientInfo == null)
            {
                return NotFound();
            }

            db.PatientInfo.Remove(patientInfo);
            db.SaveChanges();

            return Ok(patientInfo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientInfoExists(int id)
        {
            return db.PatientInfo.Count(e => e.id == id) > 0;
        }
    }
}