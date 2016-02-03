using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using CodeFirstStoreFunctions;

namespace NerdDinner.Models
{
    public class NerdDinnerDataContext : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP>   RSVPs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Add(new FunctionsConvention<NerdDinnerDataContext>("dbo"));
        }

        [DbFunction("CodeFirstDatabaseSchema", "DistanceBetween")]
        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            throw new NotImplementedException("Only call through LINQ expression");
        }

        public IQueryable<Dinner> NearestDinners(double latitude, double longitude)
        {
            return from d in Dinners
                   where DistanceBetween(latitude, longitude, d.Latitude, d.Longitude) < 100
                   select d;
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            var upcomingDinners = from dinner in Dinners
                                      //  where dinner.EventDate > DateTime.Now
                                  orderby dinner.EventDate
                                  select dinner;

            var dinners = from dinner in upcomingDinners
                          join i in NearestDinners(latitude, longitude) on dinner.DinnerID equals i.DinnerID
                          select dinner;

            return dinners;
        }
    }
}