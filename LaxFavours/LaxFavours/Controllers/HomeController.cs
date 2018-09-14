using LaxFavours.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LaxFavours.Controllers {
    public class HomeController : Controller {
        private readonly FavoursDbContext _favoursDbContext = new FavoursDbContext();
        public ActionResult Index() {
            
            return View();
        }
    }
}