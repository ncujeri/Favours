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
    public class HotelsController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Hotels
        [EnableQuery]
        public IQueryable<Hotel> GetHotels()
        {
            return db.Hotels;
        }

        // GET: odata/Hotels(5)
        [EnableQuery]
        public SingleResult<Hotel> GetHotel([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Hotels.Where(hotel => hotel.Id == key));
        }

        // POST: odata/Hotels
        public IHttpActionResult Post(Hotel hotel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotels.Add(hotel);
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

            return Created(hotel);
        }

        // PATCH: odata/Hotels(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Hotel> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel hotel = db.Hotels.Find(key);
            if (hotel == null)
            {
                return NotFound();
            }

            patch.Patch(hotel);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hotel);
        }

        // DELETE: odata/Hotels(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Hotel hotel = db.Hotels.Find(key);
            if (hotel == null)
            {
                return NotFound();
            }

            db.Hotels.Remove(hotel);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelExists(key))
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

        private bool HotelExists(Int32 key)
        {
            return db.Hotels.Count(e => e.Id == key) > 0;
        }
    }
}
