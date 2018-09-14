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
    public class Hotel_ImagesController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Hotel_Images
        [EnableQuery]
        public IQueryable<Hotel_Images> GetHotel_Images()
        {
            return db.Hotel_Images;
        }

        // GET: odata/Hotel_Images(5)
        [EnableQuery]
        public SingleResult<Hotel_Images> GetHotel_Images([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Hotel_Images.Where(hotel_images => hotel_images.ID == key));
        }

        // POST: odata/Hotel_Images
        public IHttpActionResult Post(Hotel_Images hotel_images)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Hotel_Images.Add(hotel_images);
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

            return Created(hotel_images);
        }

        // PATCH: odata/Hotel_Images(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Hotel_Images> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Hotel_Images hotel_images = db.Hotel_Images.Find(key);
            if (hotel_images == null)
            {
                return NotFound();
            }

            patch.Patch(hotel_images);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hotel_ImagesExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(hotel_images);
        }

        // DELETE: odata/Hotel_Images(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Hotel_Images hotel_images = db.Hotel_Images.Find(key);
            if (hotel_images == null)
            {
                return NotFound();
            }

            db.Hotel_Images.Remove(hotel_images);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Hotel_ImagesExists(key))
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

        private bool Hotel_ImagesExists(Int32 key)
        {
            return db.Hotel_Images.Count(e => e.ID == key) > 0;
        }
    }
}
