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
    [Route("api/MerchantOperatingTimes/{action}", Name = "MerchantOperatingTimesApi")]
    public class MerchantOperatingTimesController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantoperatingtimes = _context.MerchantOperatingTimes.Select(i => new {
                i.id,
                i.merchant_details_id,
                i.operation_type,
                i.start_time,
                i.end_time,
                i.create_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantoperatingtimes, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantOperatingTimes();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantOperatingTimes.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantOperatingTimes.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantOperatingTimes not found");

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
            var model = _context.MerchantOperatingTimes.FirstOrDefault(item => item.id == key);

            _context.MerchantOperatingTimes.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantOperatingTimes model, IDictionary values) {
            string ID = nameof(MerchantOperatingTimes.id);
            string MERCHANT_DETAILS_ID = nameof(MerchantOperatingTimes.merchant_details_id);
            string OPERATION_TYPE = nameof(MerchantOperatingTimes.operation_type);
            string START_TIME = nameof(MerchantOperatingTimes.start_time);
            string END_TIME = nameof(MerchantOperatingTimes.end_time);
            string CREATE_AT = nameof(MerchantOperatingTimes.create_at);
            string UPDATED_AT = nameof(MerchantOperatingTimes.updated_at);
            string STATE = nameof(MerchantOperatingTimes.state);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(MERCHANT_DETAILS_ID)) {
                model.merchant_details_id = Convert.ToInt32(values[MERCHANT_DETAILS_ID]);
            }

            if(values.Contains(OPERATION_TYPE)) {
                model.operation_type = Convert.ToString(values[OPERATION_TYPE]);
            }

            if(values.Contains(START_TIME)) {
                model.start_time = Convert.ToDateTime(values[START_TIME]);
            }

            if(values.Contains(END_TIME)) {
                model.end_time = Convert.ToDateTime(values[END_TIME]);
            }

            if(values.Contains(CREATE_AT)) {
                model.create_at = Convert.ToDateTime(values[CREATE_AT]);
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