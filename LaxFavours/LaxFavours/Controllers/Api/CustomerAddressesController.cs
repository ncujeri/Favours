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
    [Route("api/CustomeAddresses/{action}", Name = "CustomerAddressesApi")]
    public class CustomerAddressesController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var customeaddresses = _context.CustomeAddresses.Select(i => new {
                i.ID,
                i.customer_details_id,
                i.address_type,
                i.value,
                i.state,
                i.created_at,
                i.updated_at
            });
            return Request.CreateResponse(DataSourceLoader.Load(customeaddresses, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new CustomerAddress();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.CustomeAddresses.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.ID);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.CustomeAddresses.FirstOrDefault(item => item.ID == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "CustomerAddress not found");

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
            var model = _context.CustomeAddresses.FirstOrDefault(item => item.ID == key);

            _context.CustomeAddresses.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(CustomerAddress model, IDictionary values) {
            string ID = nameof(CustomerAddress.ID);
            string customer_details_id = nameof(CustomerAddress.customer_details_id);
            string ADDRESS_TYPE = nameof(CustomerAddress.address_type);
            string VALUE = nameof(CustomerAddress.value);
            string STATE = nameof(CustomerAddress.state);
            string CREATED_AT = nameof(CustomerAddress.created_at);
            string UPDATED_AT = nameof(CustomerAddress.updated_at);

            if(values.Contains(ID)) {
                model.ID = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(customer_details_id)) {
                model.customer_details_id = Convert.ToInt32(values[customer_details_id]);
            }

            if(values.Contains(ADDRESS_TYPE)) {
                model.address_type = Convert.ToString(values[ADDRESS_TYPE]);
            }

            if(values.Contains(VALUE)) {
                model.value = Convert.ToString(values[VALUE]);
            }

            if(values.Contains(STATE)) {
                model.state = Convert.ToString(values[STATE]);
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