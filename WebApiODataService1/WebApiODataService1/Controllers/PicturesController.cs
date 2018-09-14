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
    public class PicturesController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Pictures
        [EnableQuery]
        public IQueryable<Picture> GetPictures()
        {
            return db.Pictures;
        }

        // GET: odata/Pictures(5)
        [EnableQuery]
        public SingleResult<Picture> GetPicture([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Pictures.Where(picture => picture.Id == key));
        }

        // POST: odata/Pictures
        public IHttpActionResult Post(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);
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

            return Created(picture);
        }

        // PATCH: odata/Pictures(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Picture> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Picture picture = db.Pictures.Find(key);
            if (picture == null)
            {
                return NotFound();
            }

            patch.Patch(picture);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(picture);
        }

        // DELETE: odata/Pictures(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Picture picture = db.Pictures.Find(key);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(key))
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

        private bool PictureExists(Int32 key)
        {
            return db.Pictures.Count(e => e.Id == key) > 0;
        }
    }
}
