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
    public class RoomsController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Rooms
        [EnableQuery]
        public IQueryable<Room> GetRooms()
        {
            return db.Rooms;
        }

        // GET: odata/Rooms(5)
        [EnableQuery]
        public SingleResult<Room> GetRoom([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Rooms.Where(room => room.Id == key));
        }

        // POST: odata/Rooms
        public IHttpActionResult Post(Room room)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Rooms.Add(room);
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

            return Created(room);
        }

        // PATCH: odata/Rooms(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Room> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Room room = db.Rooms.Find(key);
            if (room == null)
            {
                return NotFound();
            }

            patch.Patch(room);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(room);
        }

        // DELETE: odata/Rooms(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Room room = db.Rooms.Find(key);
            if (room == null)
            {
                return NotFound();
            }

            db.Rooms.Remove(room);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RoomExists(key))
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

        private bool RoomExists(Int32 key)
        {
            return db.Rooms.Count(e => e.Id == key) > 0;
        }
    }
}
