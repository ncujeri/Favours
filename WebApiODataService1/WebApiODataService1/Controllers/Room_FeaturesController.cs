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
    public class Room_FeaturesController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Room_Features
        [EnableQuery]
        public IQueryable<Room_Features> GetRoom_Features()
        {
            return db.Room_Features;
        }

        // GET: odata/Room_Features(5)
        [EnableQuery]
        public SingleResult<Room_Features> GetRoom_Features([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Room_Features.Where(room_features => room_features.Id == key));
        }

        // POST: odata/Room_Features
        public IHttpActionResult Post(Room_Features room_features)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Room_Features.Add(room_features);
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

            return Created(room_features);
        }

        // PATCH: odata/Room_Features(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Room_Features> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room_Features room_features = db.Room_Features.Find(key);
            if (room_features == null)
            {
                return NotFound();
            }

            patch.Patch(room_features);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Room_FeaturesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(room_features);
        }

        // DELETE: odata/Room_Features(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Room_Features room_features = db.Room_Features.Find(key);
            if (room_features == null)
            {
                return NotFound();
            }

            db.Room_Features.Remove(room_features);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Room_FeaturesExists(key))
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

        private bool Room_FeaturesExists(Int32 key)
        {
            return db.Room_Features.Count(e => e.Id == key) > 0;
        }
    }
}
