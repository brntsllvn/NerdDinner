using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using NerdDinner.Models;

namespace NerdDinner.ViewModels
{
    public class DinnerFormViewModel
    {
        public Dinner Dinner { get; set; }
        public SelectList Countries { get; set; }

        public DinnerFormViewModel(Dinner dinner)
        {
            Dinner = dinner;
            Countries = new SelectList(PhoneValidator.Countries, dinner.Country);
        }
    }
}