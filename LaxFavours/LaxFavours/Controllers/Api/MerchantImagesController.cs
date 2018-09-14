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
    [Route("api/MerchantImages/{action}", Name = "MerchantImagesApi")]
    public class MerchantImagesController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantimages = _context.MerchantImages.Select(i => new {
                i.id,
                i.merchant_details_id,
                i.image_type,
                i.image_level,
                i.image_url,
                i.created_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantimages, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantImage();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantImages.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantImages.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantImage not found");

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
            var model = _context.MerchantImages.FirstOrDefault(item => item.id == key);

            _context.MerchantImages.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantImage model, IDictionary values) {
            string ID = nameof(MerchantImage.id);
            string MERCHANT_DETAILS_ID = nameof(MerchantImage.merchant_details_id);
            string IMAGE_TYPE = nameof(MerchantImage.image_type);
            string IMAGE_LEVEL = nameof(MerchantImage.image_level);
            string IMAGE_URL = nameof(MerchantImage.image_url);
            string CREATED_AT = nameof(MerchantImage.created_at);
            string UPDATED_AT = nameof(MerchantImage.updated_at);
            string STATE = nameof(MerchantImage.state);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(MERCHANT_DETAILS_ID)) {
                model.merchant_details_id = Convert.ToInt32(values[MERCHANT_DETAILS_ID]);
            }

            if(values.Contains(IMAGE_TYPE)) {
                model.image_type = Convert.ToString(values[IMAGE_TYPE]);
            }

            if(values.Contains(IMAGE_LEVEL)) {
                model.image_level = Convert.ToString(values[IMAGE_LEVEL]);
            }

            if(values.Contains(IMAGE_URL)) {
                model.image_url = Convert.ToString(values[IMAGE_URL]);
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