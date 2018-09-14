using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace LaxFavours.Models.Dtos.Controllers
{
    [Route("api/CustomerDetails/{action}", Name = "CustomerDetailsApi")]
    public class CustomerDetailsController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var customerdetails = _context.CustomerDetails.Select(i => new {
                i.id,
                i.first_name,
                i.last_name,
                i.email,
                i.contact,
                i.created_at,
                i.updated_at
            });
            return Request.CreateResponse(DataSourceLoader.Load(customerdetails, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new CustomerDetail();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.CustomerDetails.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.CustomerDetails.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "CustomerDetail not found");

            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        [HttpDelete]
        public void Delete(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.CustomerDetails.FirstOrDefault(item => item.id == key);

            _context.CustomerDetails.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(CustomerDetail model, IDictionary values) {
            string ID = nameof(CustomerDetail.id);
            string FIRST_NAME = nameof(CustomerDetail.first_name);
            string LAST_NAME = nameof(CustomerDetail.last_name);
            string EMAIL = nameof(CustomerDetail.email);
            string CONTACT = nameof(CustomerDetail.contact);
            string CREATED_AT = nameof(CustomerDetail.created_at);
            string UPDATED_AT = nameof(CustomerDetail.updated_at);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(FIRST_NAME)) {
                model.first_name = Convert.ToString(values[FIRST_NAME]);
            }

            if(values.Contains(LAST_NAME)) {
                model.last_name = Convert.ToString(values[LAST_NAME]);
            }

            if(values.Contains(EMAIL)) {
                model.email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(CONTACT)) {
                model.contact = Convert.ToDecimal(values[CONTACT]);
            }

            if(values.Contains(CREATED_AT)) {
                model.created_at = Convert.ToDateTime(values[CREATED_AT]);
            }

            if(values.Contains(UPDATED_AT)) {
                model.updated_at = Convert.ToDateTime(values[UPDATED_AT]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}