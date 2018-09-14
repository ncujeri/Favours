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
    public class Room_TypeController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Room_Type
        [EnableQuery]
        public IQueryable<Room_Type> GetRoom_Type()
        {
            return db.Room_Type;
        }

        // GET: odata/Room_Type(5)
        [EnableQuery]
        public SingleResult<Room_Type> GetRoom_Type([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Room_Type.Where(room_type => room_type.ID == key));
        }

        // POST: odata/Room_Type
        public IHttpActionResult Post(Room_Type room_type)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Room_Type.Add(room_type);
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

            return Created(room_type);
        }

        // PATCH: odata/Room_Type(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Room_Type> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room_Type room_type = db.Room_Type.Find(key);
            if (room_type == null)
            {
                return NotFound();
            }

            patch.Patch(room_type);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Room_TypeExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(room_type);
        }

        // DELETE: odata/Room_Type(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Room_Type room_type = db.Room_Type.Find(key);
            if (room_type == null)
            {
                return NotFound();
            }

            db.Room_Type.Remove(room_type);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Room_TypeExists(key))
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

        private bool Room_TypeExists(Int32 key)
        {
            return db.Room_Type.Count(e => e.ID == key) > 0;
        }
    }
}
