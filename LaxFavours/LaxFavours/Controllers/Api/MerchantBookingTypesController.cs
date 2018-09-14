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
    [Route("api/MerchantBookingTypes/{action}", Name = "MerchantBookingTypesApi")]
    public class MerchantBookingTypesController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantbookingtypes = _context.MerchantBookingTypes.Select(i => new {
                i.id,
                i.merchant_details_id,
                i.booking_type,
                i.value,
                i.created_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantbookingtypes, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantBookingType();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantBookingTypes.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantBookingTypes.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantBookingType not found");

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
            var model = _context.MerchantBookingTypes.FirstOrDefault(item => item.id == key);

            _context.MerchantBookingTypes.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantBookingType model, IDictionary values) {
            string ID = nameof(MerchantBookingType.id);
            string MERCHANT_DETAILS_ID = nameof(MerchantBookingType.merchant_details_id);
            string BOOKING_TYPE = nameof(MerchantBookingType.booking_type);
            string VALUE = nameof(MerchantBookingType.value);
            string CREATED_AT = nameof(MerchantBookingType.created_at);
            string UPDATED_AT = nameof(MerchantBookingType.updated_at);
            string STATE = nameof(MerchantBookingType.state);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(MERCHANT_DETAILS_ID)) {
                model.merchant_details_id = Convert.ToString(values[MERCHANT_DETAILS_ID]);
            }

            if(values.Contains(BOOKING_TYPE)) {
                model.booking_type = Convert.ToString(values[BOOKING_TYPE]);
            }

            if(values.Contains(VALUE)) {
                model.value = Convert.ToString(values[VALUE]);
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