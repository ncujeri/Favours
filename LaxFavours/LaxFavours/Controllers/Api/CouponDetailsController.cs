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
    [Route("api/CouponDetails/{action}", Name = "CouponDetailsApi")]
    public class CouponDetailsController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var coupondetails = _context.CouponDetails.Select(i => new {
                i.id,
                i.coupon_code,
                i.coupon_name,
                i.coupon_type,
                i.value,
                i.state,
                i.start_period,
                i.end_period,
                i.created_at
            });
            return Request.CreateResponse(DataSourceLoader.Load(coupondetails, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new CouponDetail();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.CouponDetails.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.CouponDetails.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "CouponDetail not found");

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
            var model = _context.CouponDetails.FirstOrDefault(item => item.id == key);

            _context.CouponDetails.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(CouponDetail model, IDictionary values) {
            string ID = nameof(CouponDetail.id);
            string COUPON_CODE = nameof(CouponDetail.coupon_code);
            string COUPON_NAME = nameof(CouponDetail.coupon_name);
            string COUPON_TYPE = nameof(CouponDetail.coupon_type);
            string VALUE = nameof(CouponDetail.value);
            string STATE = nameof(CouponDetail.state);
            string START_PERIOD = nameof(CouponDetail.start_period);
            string END_PERIOD = nameof(CouponDetail.end_period);
            string CREATED_AT = nameof(CouponDetail.created_at);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(COUPON_CODE)) {
                model.coupon_code = Convert.ToInt32(values[COUPON_CODE]);
            }

            if(values.Contains(COUPON_NAME)) {
                model.coupon_name = Convert.ToString(values[COUPON_NAME]);
            }

            if(values.Contains(COUPON_TYPE)) {
                model.coupon_type = Convert.ToString(values[COUPON_TYPE]);
            }

            if(values.Contains(VALUE)) {
                model.value = Convert.ToString(values[VALUE]);
            }

            if(values.Contains(STATE)) {
                model.state = Convert.ToString(values[STATE]);
            }

            if(values.Contains(START_PERIOD)) {
                model.start_period = Convert.ToDateTime(values[START_PERIOD]);
            }

            if(values.Contains(END_PERIOD)) {
                model.end_period = Convert.ToDateTime(values[END_PERIOD]);
            }

            if(values.Contains(CREATED_AT)) {
                model.created_at = Convert.ToDateTime(values[CREATED_AT]);
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