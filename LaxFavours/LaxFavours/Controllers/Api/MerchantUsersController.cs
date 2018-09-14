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
    [Route("api/MerchantUsers/{action}", Name = "MerchantUsersApi")]
    public class MerchantUsersController : ApiController
    {
        private FavoursDbContext _context = new FavoursDbContext();

        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions) {
            var merchantusers = _context.MerchantUsers.Select(i => new {
                i.id,
                i.first_name,
                i.last_name,
                i.email,
                i.contant,
                i.password,
                i.type,
                i.created_at,
                i.updated_at,
                i.state
            });
            return Request.CreateResponse(DataSourceLoader.Load(merchantusers, loadOptions));
        }

        [HttpPost]
        public HttpResponseMessage Post(FormDataCollection form) {
            var model = new MerchantUser();
            var values = JsonConvert.DeserializeObject<IDictionary>(form.Get("values"));
            PopulateModel(model, values);

            Validate(model);
            if (!ModelState.IsValid)
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, GetFullErrorMessage(ModelState));

            var result = _context.MerchantUsers.Add(model);
            _context.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.Created, result.id);
        }

        [HttpPut]
        public HttpResponseMessage Put(FormDataCollection form) {
            var key = Convert.ToInt32(form.Get("key"));
            var model = _context.MerchantUsers.FirstOrDefault(item => item.id == key);
            if(model == null)
                return Request.CreateResponse(HttpStatusCode.Conflict, "MerchantUser not found");

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
            var model = _context.MerchantUsers.FirstOrDefault(item => item.id == key);

            _context.MerchantUsers.Remove(model);
            _context.SaveChanges();
        }


        private void PopulateModel(MerchantUser model, IDictionary values) {
            string ID = nameof(MerchantUser.id);
            string FIRST_NAME = nameof(MerchantUser.first_name);
            string LAST_NAME = nameof(MerchantUser.last_name);
            string EMAIL = nameof(MerchantUser.email);
            string CONTANT = nameof(MerchantUser.contant);
            string PASSWORD = nameof(MerchantUser.password);
            string TYPE = nameof(MerchantUser.type);
            string CREATED_AT = nameof(MerchantUser.created_at);
            string UPDATED_AT = nameof(MerchantUser.updated_at);
            string STATE = nameof(MerchantUser.state);

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

            if(values.Contains(CONTANT)) {
                model.contant = Convert.ToString(values[CONTANT]);
            }

            if(values.Contains(PASSWORD)) {
                model.password = Convert.ToString(values[PASSWORD]);
            }

            if(values.Contains(TYPE)) {
                model.type = Convert.ToString(values[TYPE]);
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