using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity.Core.Objects;

namespace NerdDinner.Models
{
    public class DinnerRepository
    {
        public NerdDinnerDataContext db = new NerdDinnerDataContext();

        public IQueryable<Dinner> FinalAllDinners()
        {
            return db.Dinners;
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            return from dinner in db.Dinners
                where dinner.EventDate > DateTime.Now
                orderby dinner.EventDate
                select dinner;
        }

        public Dinner GetDinner(int id)
        {
            return db.Dinners.SingleOrDefault(d => d.DinnerID == id);
        }


        public void Add(Dinner dinner)
        {
            db.Dinners.Add(dinner);
        }

        public void Delete(Dinner dinner)
        {
            db.Dinners.Remove(dinner);
        }

        public void Save()
        {
            db.SaveChanges();
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {

            var dinners = from dinner in FindUpcomingDinners()
                          join i in db.NearestDinners(latitude, longitude)
                          on dinner.DinnerID equals i.DinnerID
                          select dinner;

            return dinners;
        }
    }
}