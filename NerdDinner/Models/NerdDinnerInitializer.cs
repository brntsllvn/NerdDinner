using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace NerdDinner.Models
{
    public class NerdDinnerInitializer : DropCreateDatabaseIfModelChanges<NerdDinnerDataContext>
    {
        protected override void Seed(NerdDinnerDataContext context)
        {
            var dinners = new List<Dinner>
            {
                new Dinner
                {
                    Title = "Sample Dinner 1",
                    EventDate = DateTime.Parse("12/31/2016"),
                    Address = "One MSFT Wy",
                    Country = "USA",
                    HostedBy = "scott.gu@ms.com"
                },

                new Dinner
                {
                    Title = "Sample Dinner 2",
                    EventDate = DateTime.Parse("10/31/2016"),
                    Address = "One MSFT Wy",
                    Country = "DEU",
                    HostedBy = "guey.Scots@ms.com"
                }
            };

            dinners.ForEach(d => context.Dinners.Add(d));

            context.SaveChanges();
        }
    }
}