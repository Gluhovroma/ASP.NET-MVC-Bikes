using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace BikeLivery.Models
{
    public class BikesModelContext : DbContext
    {
         public BikesModelContext()
            : base("BikesContext2")
        {
        }
         public DbSet<Clients> Clients { get; set; }
         public DbSet<Classes> Classes { get; set; }
         public DbSet<Orders> Orders { get; set; }
         public DbSet<Bikes> Bikes { get; set; }
         public DbSet<Liveries> Liveries { get; set; }
         public DbSet<Status> Statuses { get; set; }

    }
    public class Clients
    {
        [Key]
        public int ClientID { get; set; }
        public string SurName { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Passport { get; set; }
    }
    public class Classes
    {
        [Key]
        public int ClassID { get; set; }
        public string ClassName { get; set; }        
    }
    public class Orders
    {
        [Key]
        public int OrderID { get; set; }
        public int BikeID { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ClientID { get; set; }
             
        public int LiveryStart { get; set; }
        public int LiveryEnd { get; set; }
        public string TotalPrice { get; set; }
        public int StatusID { get; set; } 
    }
    public class Bikes
    {
        [Key]
        public int BikeID { get; set; }
        public int ClassID { get; set; }
        public string PricePerHour { get; set; }
        public string Model { get; set; }
        public string InventoryNumber { get; set; }
    }
    public class Liveries
    {
        [Key]
        public int LiveryID { get; set; }        
        public string Address { get; set; }        
    }
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }
    public class OrderHistory
    {
        [Key]
        public int OrderID { get; set; }
        public int BikeID { get; set; }
        public string ModelName { get; set; }
        public string ClassName { get; set; }
        public string Client { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string LiveryStart { get; set; }
        public string LiveryEnd { get; set; }
        public string Status { get; set; }
        public string TotalPrice { get; set; }
        public string InventoryNumber { get; set; }
        
    }
    public class BikesFilter
    {
        [Key]
        public int BikeID { get; set; }
        public string InventoryNum { get; set; }
        
    }
    public class JsonParseEdit
    { 
        [Key]
        public string ID { get; set; }
        public string Value { get; set; }
        public string OldID { get; set; }
    }
    public class JsonParseDelete
    {
        [Key]
        public string IDToDelete { get; set; }
        
    }
    public class JsonParseAdd
    {
        public string BikeIDNew { get; set; }
        public string DateTimeStartNew { get; set; }
        public string DateTimeEndNew { get; set; }        
        public string ClientOrdIDNew { get; set; } 
        public string LiveryStartIDNew { get; set; }
        public string LiveryEndIDNew { get; set; }
        public string StatusIDNew { get; set; }
        public string TotalPriceNew { get; set; }
              
    }
}