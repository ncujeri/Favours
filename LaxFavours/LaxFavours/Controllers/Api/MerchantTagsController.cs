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
    [Route("api/MerchantTags/{action}", Name = "MerchantTagsApi")]
    public class MerchantTagsController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchanttags = _context.MerchantTags.Select(i => new {
                i.id,
                i.merchant_details_id,
                i.tag_type,
                i.tag_level,
                i.tag_value,
                i.created_at,
                i.updated_at
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchanttags, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantTag();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantTags.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantTags.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantTag not found");

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
            var model = _context.MerchantTags.FirstOrDefault(item => item.id == key);

            _context.MerchantTags.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantTag model, IDictionary values) {
            string ID = nameof(MerchantTag.id);
            string MERCHANT_DETAILS_ID = nameof(MerchantTag.merchant_details_id);
            string TAG_TYPE = nameof(MerchantTag.tag_type);
            string TAG_LEVEL = nameof(MerchantTag.tag_level);
            string TAG_VALUE = nameof(MerchantTag.tag_value);
            string CREATED_AT = nameof(MerchantTag.created_at);
            string UPDATED_AT = nameof(MerchantTag.updated_at);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(MERCHANT_DETAILS_ID)) {
                model.merchant_details_id = Convert.ToInt32(values[MERCHANT_DETAILS_ID]);
            }

            if(values.Contains(TAG_TYPE)) {
                model.tag_type = Convert.ToString(values[TAG_TYPE]);
            }

            if(values.Contains(TAG_LEVEL)) {
                model.tag_level = Convert.ToString(values[TAG_LEVEL]);
            }

            if(values.Contains(TAG_VALUE)) {
                model.tag_value = Convert.ToString(values[TAG_VALUE]);
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