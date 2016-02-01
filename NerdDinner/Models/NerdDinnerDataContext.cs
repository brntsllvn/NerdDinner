using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class NerdDinnerDataContext : DbContext
    {
        public DbSet<Dinner> Dinners { get; set; }
        public DbSet<RSVP>   RSVPs { get; set; }
    }
}