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
    [Route("api/MerchantDetails/{action}", Name = "MerchantDetailsApi")]
    public class MerchantDetailsController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantdetails = _context.MerchantDetails.Select(i => new {
                i.id,
                i.name,
                i.email,
                i.contant,
                i.short_description,
                i.full_description,
                i.created_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantdetails, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantDetail();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantDetails.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantDetails.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantDetail not found");

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
            var model = _context.MerchantDetails.FirstOrDefault(item => item.id == key);

            _context.MerchantDetails.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantDetail model, IDictionary values) {
            string ID = nameof(MerchantDetail.id);
            string NAME = nameof(MerchantDetail.name);
            string EMAIL = nameof(MerchantDetail.email);
            string CONTANT = nameof(MerchantDetail.contant);
            string SHORT_DESCRIPTION = nameof(MerchantDetail.short_description);
            string FULL_DESCRIPTION = nameof(MerchantDetail.full_description);
            string CREATED_AT = nameof(MerchantDetail.created_at);
            string UPDATED_AT = nameof(MerchantDetail.updated_at);
            string STATE = nameof(MerchantDetail.state);

            if(values.Contains(ID)) {
                model.id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(EMAIL)) {
                model.email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(CONTANT)) {
                model.contant = Convert.ToString(values[CONTANT]);
            }

            if(values.Contains(SHORT_DESCRIPTION)) {
                model.short_description = Convert.ToString(values[SHORT_DESCRIPTION]);
            }

            if(values.Contains(FULL_DESCRIPTION)) {
                model.full_description = Convert.ToString(values[FULL_DESCRIPTION]);
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