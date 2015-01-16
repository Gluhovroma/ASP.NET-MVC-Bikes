using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BikeLivery.Models;
using System.Data;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using BikeLivery.Filters;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;


namespace BikeLivery.Controllers
{
    public class HomeController : Controller
    {
        BikesModelContext db = new BikesModelContext();

        [InitializeSimpleMembership]

        public ActionResult Index()
        {
            ViewBag.Title = "Добро пожаловать в автоматизированную систему проката велосипедов";
            var Orders = db.Orders.ToList();
            var Client = db.Clients.ToList();
            var Liveries = db.Liveries.ToList();
            var Bikes = db.Bikes.ToList();
            var Classes = db.Classes.ToList();
            var Statuses = db.Statuses.ToList();
            ViewBag.Orders = Orders.Join(Client, s => s.ClientID,
                                                   c => c.ClientID,
                                                   (s, c) => new { s, c })
                                      .Join(Liveries, li => li.s.LiveryStart,
                                                      liv => liv.LiveryID,
                                                      (li, liv) => new { li, liv })
                                      .Join(Liveries, live => live.li.s.LiveryEnd,
                                                      liver => liver.LiveryID,
                                                      (live, liver) => new { live, liver })
                                      .Join(Bikes, bi => bi.live.li.s.BikeID,
                                                   bik => bik.BikeID,
                                                   (bi, bik) => new { bi, bik })
                                      .Join(Classes, cl => cl.bik.ClassID,
                                                     cla => cla.ClassID,
                                                     (cl, cla) => new { cl, cla })
                                      .Join(Statuses, st => st.cl.bi.live.li.s.StatusID,
                                                     sta => sta.StatusID,
                                                     (st, sta) => new {st, sta })
                                      .ToList()
                                      .Select(ord => new OrderHistory
                                      {
                                          OrderID = ord.st.cl.bi.live.li.s.OrderID,
                                          BikeID = ord.st.cl.bi.live.li.s.BikeID,
                                          ClassName = ord.st.cla.ClassName,
                                          Client = ord.st.cl.bi.live.li.c.SurName,
                                          StartDateTime = ord.st.cl.bi.live.li.s.StartDateTime,
                                          EndDateTime = ord.st.cl.bi.live.li.s.EndDateTime,
                                          LiveryStart = ord.st.cl.bi.live.liv.Address,
                                          LiveryEnd = ord.st.cl.bi.liver.Address,
                                          ModelName = ord.st.cl.bik.Model,
                                          TotalPrice = ord.st.cl.bi.live.li.s.TotalPrice,
                                          InventoryNumber = ord.st.cl.bik.InventoryNumber,
                                          Status = ord.sta.StatusName
                                      });
            ViewBag.Bikes = Bikes.Select(Bike => new BikesFilter {BikeID = Bike.BikeID, 
                                                                  InventoryNum =Bike.InventoryNumber
                                                                  });                
            return View();
        }
        public ActionResult Edit()
        {
            ViewBag.DateTimeNow = DateTime.Now;
            ViewBag.Message = "Your app description page.";
            var Orders = db.Orders.ToList();
            var Client = db.Clients.ToList();
            var Liveries = db.Liveries.ToList();
            var Bikes = db.Bikes.ToList();
            var Classes = db.Classes.ToList();
            var Statuses = db.Statuses.ToList();
            ViewBag.Bikes = Bikes;
            ViewBag.Orderss = Orders;
            ViewBag.Clients = Client;
            ViewBag.Liveries = Liveries;
            ViewBag.Statuses = Statuses;

            ViewBag.Orders = Orders.Join(Client, s => s.ClientID,
                                                   c => c.ClientID,
                                                   (s, c) => new { s, c })
                                      .Join(Liveries, li => li.s.LiveryStart,
                                                      liv => liv.LiveryID,
                                                      (li, liv) => new { li, liv })
                                      .Join(Liveries, live => live.li.s.LiveryEnd,
                                                      liver => liver.LiveryID,
                                                      (live, liver) => new { live, liver })
                                      .Join(Bikes, bi => bi.live.li.s.BikeID,
                                                   bik => bik.BikeID,
                                                   (bi, bik) => new { bi, bik })
                                      .Join(Classes, cl => cl.bik.ClassID,
                                                     cla => cla.ClassID,
                                                     (cl, cla) => new { cl, cla })
                                      .Join(Statuses, st => st.cl.bi.live.li.s.StatusID,
                                                     sta => sta.StatusID,
                                                     (st, sta) => new { st, sta })
                                      .ToList()
                                      .Select(ord => new OrderHistory
                                      {
                                          OrderID = ord.st.cl.bi.live.li.s.OrderID,
                                          BikeID = ord.st.cl.bi.live.li.s.BikeID,
                                          ClassName = ord.st.cla.ClassName,
                                          Client = ord.st.cl.bi.live.li.c.SurName,
                                          StartDateTime = ord.st.cl.bi.live.li.s.StartDateTime,
                                          EndDateTime = ord.st.cl.bi.live.li.s.EndDateTime,
                                          LiveryStart = ord.st.cl.bi.live.liv.Address,
                                          LiveryEnd = ord.st.cl.bi.liver.Address,
                                          ModelName = ord.st.cl.bik.Model,
                                          TotalPrice = ord.st.cl.bi.live.li.s.TotalPrice,
                                          InventoryNumber = ord.st.cl.bik.InventoryNumber,
                                          Status = ord.sta.StatusName
                                      });
            return View();
        }

        [HttpPost]
        public ActionResult FilterByBike()
        {
            ViewBag.Title = "Добро пожаловать в автоматизированную систему проката велосипедов";
            int ID;
            int.TryParse(Request.Form["BikeFilter"], out ID);
            var Orders = db.Orders.ToList();
            var Client = db.Clients.ToList();
            var Liveries = db.Liveries.ToList();
            var Bikes = db.Bikes.ToList();
            var Classes = db.Classes.ToList();
            var Statuses = db.Statuses.ToList();
            ViewBag.Orders = Orders.Join(Client, s => s.ClientID,
                                                   c => c.ClientID,
                                                   (s, c) => new { s, c })
                                      .Join(Liveries, li => li.s.LiveryStart,
                                                      liv => liv.LiveryID,
                                                      (li, liv) => new { li, liv })
                                      .Join(Liveries, live => live.li.s.LiveryEnd,
                                                      liver => liver.LiveryID,
                                                      (live, liver) => new { live, liver })
                                      .Join(Bikes, bi => bi.live.li.s.BikeID,
                                                   bik => bik.BikeID,
                                                   (bi, bik) => new { bi, bik })
                                      .Join(Classes, cl => cl.bik.ClassID,
                                                     cla => cla.ClassID,
                                                     (cl, cla) => new { cl, cla })
                                      .Join(Statuses, st => st.cl.bi.live.li.s.StatusID,
                                                     sta => sta.StatusID,
                                                     (st, sta) => new { st, sta })
                                      .ToList()
                                      .Select(ord => new OrderHistory
                                      {
                                          OrderID = ord.st.cl.bi.live.li.s.OrderID,
                                          BikeID = ord.st.cl.bi.live.li.s.BikeID,
                                          ClassName = ord.st.cla.ClassName,
                                          Client = ord.st.cl.bi.live.li.c.SurName,
                                          StartDateTime = ord.st.cl.bi.live.li.s.StartDateTime,
                                          EndDateTime = ord.st.cl.bi.live.li.s.EndDateTime,
                                          LiveryStart = ord.st.cl.bi.live.liv.Address,
                                          LiveryEnd = ord.st.cl.bi.liver.Address,
                                          ModelName = ord.st.cl.bik.Model,
                                          TotalPrice = ord.st.cl.bi.live.li.s.TotalPrice,
                                          InventoryNumber = ord.st.cl.bik.InventoryNumber,
                                          Status = ord.sta.StatusName
                                      })
                                       .Where(Orderss => (ID >= 0) ? Orderss.BikeID == ID : Orderss.BikeID >= 0);
            
            ViewBag.Bikes = Bikes.Select(Bike => new BikesFilter
            {
                BikeID = Bike.BikeID, 
                InventoryNum = Bike.InventoryNumber                
            });                  
            return PartialView("AjaxView");
        }
        public ActionResult Contact()
        {            
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        public ActionResult DataEdit(string strData)
        {
            JsonParseEdit jParse =  new JavaScriptSerializer().Deserialize<JsonParseEdit>(strData);
            var Orders = db.Orders.ToList();
            if (jParse.ID == "InventoryNum")
            {
                int BikeIDToChange;
                int BikeIDNew;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                int.TryParse(jParse.Value, out BikeIDNew);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.BikeID = BikeIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
                //db.Orders(NewOrder)
                //var NewOrder = db.Orders.Where(Orders => Orders.OrderID == OrderIDToChange).ToList();
              //  db.Orders.
            }
            if (jParse.ID == "DateTimeStart")
            {
                int BikeIDToChange;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                DateTime NewDateTime = Convert.ToDateTime(jParse.Value);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.StartDateTime = NewDateTime;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "DateTimeEnd")
            {
                int BikeIDToChange;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                DateTime NewDateTime = Convert.ToDateTime(jParse.Value);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.EndDateTime = NewDateTime;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "Client")
            {
                int BikeIDToChange;
                int ClientIDNew;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                int.TryParse(jParse.Value, out ClientIDNew);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.ClientID = ClientIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "LiveryStart")
            {
                int BikeIDToChange;
                int LiveryStartIDNew;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                int.TryParse(jParse.Value, out LiveryStartIDNew);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.LiveryStart = LiveryStartIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "LiveryEnd")
            {
                int BikeIDToChange;
                int LiveryEndIDNew;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                int.TryParse(jParse.Value, out LiveryEndIDNew);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.LiveryEnd = LiveryEndIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "Status")
            {
                int BikeIDToChange;
                int StatusIDNew;
                int.TryParse(jParse.OldID, out BikeIDToChange);
                int.TryParse(jParse.Value, out StatusIDNew);
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.StatusID = StatusIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            if (jParse.ID == "TotalPrice")
            {
                int BikeIDToChange;
                string TotalPriceIDNew = jParse.Value;
                int.TryParse(jParse.OldID, out BikeIDToChange);                
                Orders NewOrder = Orders.SingleOrDefault(Orderss => Orderss.OrderID == BikeIDToChange);
                NewOrder.TotalPrice = TotalPriceIDNew;
                db.Entry(NewOrder).State = EntityState.Modified;
                db.SaveChanges();
            }
            ViewBag.Message = "Your contact page.";            
            return View();
        }
        [HttpPost]
        public ActionResult DataDelete(string strDataDelete)
        {
            var Orders = db.Orders.ToList();
            JsonParseDelete jParseDelete = new JavaScriptSerializer().Deserialize<JsonParseDelete>(strDataDelete);
            int OrderToDeleteID;
            int.TryParse(jParseDelete.IDToDelete, out OrderToDeleteID);
            Orders OrderToDelete = Orders.SingleOrDefault(Orderss => Orderss.OrderID == OrderToDeleteID);
            db.Orders.Remove(OrderToDelete);
            db.SaveChanges();
            ViewBag.Message = "Your contact page.";
            return View();
        }
        [HttpPost]
        public ActionResult DataAdd(string strDataToAdd)
        {
            var Orders = db.Orders.ToList();
            JsonParseAdd jParseAdd = new JavaScriptSerializer().Deserialize<JsonParseAdd>(strDataToAdd);
            int BikeIDNew;
            DateTime DateTimeStartNew;
            DateTime DateTimeEndNew;        
            int ClientOrdIDNew;
            int LiveryStartIDNew;
            int LiveryEndIDNew;
            int StatusIDNew;
            string TotalPriceNew;
            int.TryParse(jParseAdd.BikeIDNew, out BikeIDNew);
            DateTimeStartNew = Convert.ToDateTime(jParseAdd.DateTimeStartNew);
            DateTimeEndNew = Convert.ToDateTime(jParseAdd.DateTimeEndNew);
            int.TryParse(jParseAdd.ClientOrdIDNew, out ClientOrdIDNew);
            int.TryParse(jParseAdd.LiveryStartIDNew, out LiveryStartIDNew);
            int.TryParse(jParseAdd.LiveryEndIDNew, out LiveryEndIDNew);
            int.TryParse(jParseAdd.StatusIDNew, out StatusIDNew);
            TotalPriceNew = jParseAdd.TotalPriceNew;
            Orders OrderToAdd = new Orders();
            OrderToAdd.BikeID = BikeIDNew;
            OrderToAdd.StartDateTime = DateTimeStartNew;
            OrderToAdd.EndDateTime = DateTimeEndNew;
            OrderToAdd.ClientID = ClientOrdIDNew;
            OrderToAdd.LiveryStart = LiveryStartIDNew;
            OrderToAdd.LiveryEnd = LiveryEndIDNew;
            OrderToAdd.StatusID = StatusIDNew;
            OrderToAdd.TotalPrice = TotalPriceNew;
            //Orders OrderToDelete = Orders.SingleOrDefault(Orderss => Orderss.OrderID == OrderToDeleteID);
            //db.Orders.Remove(OrderToDelete);
            //db.SaveChanges();
            db.Orders.Add(OrderToAdd);
            db.SaveChanges();
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}
