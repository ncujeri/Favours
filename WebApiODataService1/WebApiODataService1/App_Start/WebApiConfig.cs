using System;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.OData.Builder;
using System.Web.OData.Extensions;
using Microsoft.Owin.Security.OAuth;

namespace WebApiODataService1 {
    public static class WebApiConfig {
        public static void Register(HttpConfiguration config) {
            // Web API configuration and services
            // Configure Web API to use only bearer token authentication.
            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.SetTimeZoneInfo(TimeZoneInfo.Utc);

            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();

            builder.EntitySet<City>("Cities");
            builder.EntityType<City>().HasKey(entity => entity.ID);

            builder.EntitySet<Features_List>("Features_List");
            builder.EntityType<Features_List>().HasKey(entity => entity.ID);

            builder.EntitySet<Guest>("Guests");
            builder.EntityType<Guest>().HasKey(entity => entity.Id);

            builder.EntitySet<Hotel>("Hotels");
            builder.EntityType<Hotel>().HasKey(entity => entity.Id);

            builder.EntitySet<Hotel_Features>("Hotel_Features");
            builder.EntityType<Hotel_Features>().HasKey(entity => entity.ID);

            builder.EntitySet<Hotel_Images>("Hotel_Images");
            builder.EntityType<Hotel_Images>().HasKey(entity => entity.ID);

            builder.EntitySet<Picture>("Pictures");
            builder.EntityType<Picture>().HasKey(entity => entity.Id);

            builder.EntitySet<Reservation>("Reservations");
            builder.EntityType<Reservation>().HasKey(entity => entity.Id);

            builder.EntitySet<Review>("Reviews");
            builder.EntityType<Review>().HasKey(entity => entity.Id);

            builder.EntitySet<Room>("Rooms");
            builder.EntityType<Room>().HasKey(entity => entity.Id);

            builder.EntitySet<Room_Features>("Room_Features");
            builder.EntityType<Room_Features>().HasKey(entity => entity.Id);

            builder.EntitySet<Room_Type>("Room_Type");
            builder.EntityType<Room_Type>().HasKey(entity => entity.ID);

            config.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());

            config.Formatters.JsonFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("text/html"));
        }
    }
}
