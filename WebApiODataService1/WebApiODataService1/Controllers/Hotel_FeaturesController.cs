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
    public class Hotel_FeaturesController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Hotel_Features
        [EnableQuery]
        public IQueryable<Hotel_Features> GetHotel_Features()
        {
            return db.Hotel_Features;
        }

        // GET: odata/Hotel_Features(5)
        [EnableQuery]
        public SingleResult<Hotel_Features> GetHotel_Features([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Hotel_Features.Where(hotel_features => hotel_features.ID == key));
        }

        // POST: odata/Hotel_Features
        public IHttpActionResult Post(Hotel_Features hotel_features)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotel_Features.Add(hotel_features);
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

            return Created(hotel_features);
        }

        // PATCH: odata/Hotel_Features(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Hotel_Features> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel_Features hotel_features = db.Hotel_Features.Find(key);
            if (hotel_features == null)
            {
                return NotFound();
            }

            patch.Patch(hotel_features);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hotel_FeaturesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hotel_features);
        }

        // DELETE: odata/Hotel_Features(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Hotel_Features hotel_features = db.Hotel_Features.Find(key);
            if (hotel_features == null)
            {
                return NotFound();
            }

            db.Hotel_Features.Remove(hotel_features);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hotel_FeaturesExists(key))
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

        private bool Hotel_FeaturesExists(Int32 key)
        {
            return db.Hotel_Features.Count(e => e.ID == key) > 0;
        }
    }
}
