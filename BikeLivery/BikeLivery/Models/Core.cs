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
    public class BikesContext : DbContext
    {
        public BikesContext()
            : base("BikesContext")
        {
        }
        public DbSet<Bikes> Bikes { get; set; }
        public DbSet<Marks> Marks { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Spots> Spots { get; set; }
    }
    
    
    public class Bikes
    {
        [Key]
        public int BikeId { get; set; }
        public int BikeMark { get; set; }
    }
   
    public class Orders
    {
        [Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int OrderId { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime TimeEnd { get; set; }
        public int StartSpot { get; set; }
        public int EndSpot { get; set; }
        public int status { get; set; }
        public int BikeId { get; set; }

    }

    public class Spots
    {
        [Key]
        public int SpotId { get; set; }
        public string Location { get; set; }

    }
    public class Marks
    {
        [Key]
        public int MarkId { get; set; }
        public string Name { get; set; }

    }
    public class Histoty
    {
        [Key]
        public int bikes { get; set; }

        public string mark { get; set; }
        public DateTime start_t { get; set; }
        public DateTime end_t { get; set; }
        public int status { get; set; }
        public string startloc { get; set; }
        public string endloc { get; set; }
    }
    public class BikesFilter
    {
        [Key]
        public int BikeId { get; set; }
        public string Name { get; set; }
    }
}