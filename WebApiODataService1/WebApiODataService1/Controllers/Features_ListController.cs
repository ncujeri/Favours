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
    public class Features_ListController : ODataController
    {
        private DataModel db = new DataModel();

        // GET: odata/Features_List
        [EnableQuery]
        public IQueryable<Features_List> GetFeatures_List()
        {
            return db.Features_List;
        }

        // GET: odata/Features_List(5)
        [EnableQuery]
        public SingleResult<Features_List> GetFeatures_List([FromODataUri] Int32 key)
        {
            return SingleResult.Create(db.Features_List.Where(features_list => features_list.ID == key));
        }

        // POST: odata/Features_List
        public IHttpActionResult Post(Features_List features_list)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Features_List.Add(features_list);
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

            return Created(features_list);
        }

        // PATCH: odata/Features_List(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<Features_List> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Features_List features_list = db.Features_List.Find(key);
            if (features_list == null)
            {
                return NotFound();
            }

            patch.Patch(features_list);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Features_ListExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(features_list);
        }

        // DELETE: odata/Features_List(5)
        public IHttpActionResult Delete([FromODataUri] Int32 key)
        {
            Features_List features_list = db.Features_List.Find(key);
            if (features_list == null)
            {
                return NotFound();
            }

            db.Features_List.Remove(features_list);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Features_ListExists(key))
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

        private bool Features_ListExists(Int32 key)
        {
            return db.Features_List.Count(e => e.ID == key) > 0;
        }
    }
}
