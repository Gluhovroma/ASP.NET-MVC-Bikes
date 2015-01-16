using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeLivery.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BikeLivery.Filters;
namespace BikeLivery.Controllers
{
    public class HomeController : Controller
    {
        
        BikesContext db = new BikesContext();

        
        [InitializeSimpleMembership]
        public ActionResult Index()
        {
            ViewBag.Title = "Добро пожаловать в автоматизированную систему проката велосипедов";
            var Orders = from orders in db.Orders
                         join spots in db.Spots on orders.StartSpot equals spots.SpotId
                         join bikes in db.Bikes on orders.BikeId equals bikes.BikeId
                         join marks in db.Marks on bikes.BikeMark equals marks.MarkId
                         select new { bikes = orders.BikeId, mark = marks.Name, strat_t = orders.TimeStart, end_t = orders.TimeEnd, status = orders.status, startloc = spots.Location, orders.EndSpot };
            ViewBag.orders = from orders in Orders.AsQueryable()
                             join spots in db.Spots on orders.EndSpot equals spots.SpotId
                             select new Histoty{ bikes=orders.bikes, mark=orders.mark, start_t=orders.strat_t, end_t=orders.end_t, status=orders.status, startloc=orders.startloc, endloc = spots.Location };
            ViewBag.bikes = from bikes in db.Bikes
                            join marks in db.Marks on bikes.BikeMark equals marks.MarkId
                            select new BikesFilter {BikeId= bikes.BikeId, Name=marks.Name };

            return View();
        }
        [HttpPost]
        public ActionResult FilterByBike()
        {
            var Orders = from orders in db.Orders
                         join spots in db.Spots on orders.StartSpot equals spots.SpotId
                         join bikes in db.Bikes on orders.BikeId equals bikes.BikeId
                         join marks in db.Marks on bikes.BikeMark equals marks.MarkId
                         select new { bikes = orders.BikeId, mark = marks.Name, strat_t = orders.TimeStart, end_t = orders.TimeEnd, status = orders.status, startloc = spots.Location, orders.EndSpot };
            ViewBag.orders = from orders in Orders.AsQueryable()
                             join spots in db.Spots on orders.EndSpot equals spots.SpotId
                             select new Histoty { bikes = orders.bikes, mark = orders.mark, start_t = orders.strat_t, end_t = orders.end_t, status = orders.status, startloc = orders.startloc, endloc = spots.Location };
            ViewBag.bikes = from bikes in db.Bikes
                            join marks in db.Marks on bikes.BikeMark equals marks.MarkId
                            select new BikesFilter { BikeId = bikes.BikeId, Name = marks.Name };
            return View();
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
