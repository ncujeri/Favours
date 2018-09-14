using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.ModelBinding;
using System.Web.OData;
using System.Web.OData.Query;
using System.Web.OData.Routing;


namespace WebApiODataService1 {
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.OData.Builder;
    using System.Web.OData.Extensions;
    using WebApiODataService1;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class GuestsController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Guests
        [EnableQuery]
        public IQueryable<Guest> GetGuests()
        {
            return db.Guests;
        }

        // GET: odata/Guests(5)
        [EnableQuery]
        public SingleResult<Guest> GetGuest([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Guests.Where(guest => guest.Id == key));
        }

        // POST: odata/Guests
        public IHttpActionResult Post(Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Guests.Add(guest);
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }
                throw new System.Data.Entity.Validation.DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex );
            }

            return Created(guest);
        }

        // PATCH: odata/Guests(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Guest> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Guest guest = db.Guests.Find(key);
            if (guest == null)
            {
                return NotFound();
            }

            patch.Patch(guest);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(guest);
        }

        // DELETE: odata/Guests(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Guest guest = db.Guests.Find(key);
            if (guest == null)
            {
                return NotFound();
            }

            db.Guests.Remove(guest);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GuestExists(key))
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


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GuestExists(Int32 key)
        {
            return db.Guests.Count(e => e.Id == key) > 0;
        }
    }
}
