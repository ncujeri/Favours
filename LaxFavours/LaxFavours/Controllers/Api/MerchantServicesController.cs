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
    [Route("api/MerchantServices/{action}", Name = "MerchantServicesApi")]
    public class MerchantServicesController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantservices = _context.MerchantServices.Select(i => new {
                i.id,
                i.merchant_details_id,
                i.service_name,
                i.short_description,
                i.full_description,
                i.logistics,
                i.price,
                i.created_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantservices, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantService();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantServices.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantServices.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantService not found");

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
            var model = _context.MerchantServices.FirstOrDefault(item => item.id == key);

            _context.MerchantServices.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantService model, IDictionary values) {
            string ID = nameof(MerchantService.id);
            string MERCHANT_DETAILS_ID = nameof(MerchantService.merchant_details_id);
            string SERVICE_NAME = nameof(MerchantService.service_name);
            string SHORT_DESCRIPTION = nameof(MerchantService.short_description);
            string FULL_DESCRIPTION = nameof(MerchantService.full_description);
            string LOGISTICS = nameof(MerchantService.logistics);
            string PRICE = nameof(MerchantService.price);
            string CREATED_AT = nameof(MerchantService.created_at);
            string UPDATED_AT = nameof(MerchantService.updated_at);
            string STATE = nameof(MerchantService.state);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(MERCHANT_DETAILS_ID)) {
                model.merchant_details_id = Convert.ToInt32(values[MERCHANT_DETAILS_ID]);
            }

            if(values.Contains(SERVICE_NAME)) {
                model.service_name = Convert.ToString(values[SERVICE_NAME]);
            }

            if(values.Contains(SHORT_DESCRIPTION)) {
                model.short_description = Convert.ToString(values[SHORT_DESCRIPTION]);
            }

            if(values.Contains(FULL_DESCRIPTION)) {
                model.full_description = Convert.ToString(values[FULL_DESCRIPTION]);
            }

            if(values.Contains(LOGISTICS)) {
                model.logistics = Convert.ToString(values[LOGISTICS]);
            }

            if(values.Contains(PRICE)) {
                model.price = Convert.ToDecimal(values[PRICE]);
            }

            if(values.Contains(CREATED_AT)) {
                model.created_at = Convert.ToDateTime(values[CREATED_AT]);
            }

            if(values.Contains(UPDATED_AT)) {
                model.updated_at = Convert.ToDateTime(values[UPDATED_AT]);
            }

            if(values.Contains(STATE)) {
                model.state = Convert.ToString(values[STATE]);
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