using LaxFavours.Models;
using LaxFavours.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.OData;

namespace LaxFavours.Controllers.OData
{
    public class CustomerDetailsController : ODataController
    {
        #region Private Properties
        private readonly FavoursDbContext _db = new FavoursDbContext();

        #endregion

        #region Public Methods
        [EnableQuery]
        public IQueryable<CustomerDetail> GetCustomerDetails()
        {
            return _db.CustomerDetails;
        }
        [EnableQuery]
        public SingleResult<CustomerDetail> GetCustomerDetails([FromODataUri] Int32 key)
        {
            return SingleResult.Create(_db.CustomerDetails.Where(
                c => c.id == key));
        }
        public IHttpActionResult Post(CustomerDetail customer)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _db.CustomerDetails.Add(customer);
            try
            {
                _db.SaveChanges();
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
                throw new System.Data.Entity.Validation.DbEntityValidationException("Entity Validation Failed - errors follow:\n" + sb.ToString(), ex);
            }
            return Created(customer);


        }
        [System.Web.Http.AcceptVerbs("PATCH","MERGE")]
        public IHttpActionResult Patch([FromODataUri] Int32 key, Delta<CustomerDetail> patch)
        {
            Validate(patch.GetEntity());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            CustomerDetail customerDetail = _db.CustomerDetails.Find(key);
            if (customerDetail == null)
            {
                return NotFound();
            }
            patch.Patch(customerDetail);
            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(key))
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
        #endregion
        #region Private Methods
        private bool CustomerExists(Int32 key)
        {
            return _db.CustomerDetails.Count(e => e.id == key) > 0;
        }
        #endregion
    }
}