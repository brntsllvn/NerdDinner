using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using NerdDinner.Models;
using NerdDinner.ViewModels;
using NerdDinner.Helpers;

namespace NerdDinner.Controllers
{

    public class DinnersController : Controller
    {

        DinnerRepository dinnerRepository = new DinnerRepository();

        // GET: /Dinners/
        public ActionResult Index(int? page)
        {
            const int pageSize = 3;

            var upcomingDinners = dinnerRepository.FindUpcomingDinners();
            var paginatedDinners = new PaginatedList<Dinner>(upcomingDinners, page ?? 0, pageSize);
            return View("Index", paginatedDinners);
        }

        // GET: /Dinners/Details/2
        public ActionResult Details(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View("Details", dinner);
        }

        [Authorize]
        public ActionResult Create()
        {
            Dinner dinner = new Dinner()
            {
                EventDate = DateTime.Now.AddDays(7)
            };

            return View(new DinnerFormViewModel(dinner));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Create(Dinner dinner)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    dinner.HostedBy = User.Identity.Name;

                    dinnerRepository.Add(dinner);
                    dinnerRepository.Save();

                    return RedirectToAction("Details", new {id = dinner.DinnerID});
                }
                catch
                {
                    foreach (var issue in dinner.GetRuleViolations())
                    {
                        ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                    }
                }
            }
            return View(new DinnerFormViewModel(dinner));
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsHostedBy(User.Identity.Name))
            {
                return View("InvalidOwner");
            }

            return View(new DinnerFormViewModel(dinner));
        }

        [HttpPost]
        [Authorize]
        public ActionResult Edit(int id, FormCollection collection)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            try
            {
                UpdateModel(dinner, "Dinner");
                dinnerRepository.Save();
                return RedirectToAction("Details", new {id = dinner.DinnerID});
            }
            catch
            {
                foreach (var issue in dinner.GetRuleViolations())
                {
                    ModelState.AddModelError(issue.PropertyName, issue.ErrorMessage);
                }
                return View(new DinnerFormViewModel(dinner));
            }
        }

        public ActionResult Delete(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");
            else
                return View(dinner);
        }

        [HttpPost]
        public ActionResult Delete(int id, int? dummyid)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (dinner == null)
                return View("NotFound");

            dinnerRepository.Delete(dinner);
            dinnerRepository.Save();

            return View("Deleted");
        }

        [HttpPost]
        [Authorize]
        public ActionResult Register(int id)
        {
            Dinner dinner = dinnerRepository.GetDinner(id);

            if (!dinner.IsUserRegistered(User.Identity.Name))
            {
                RSVP rsvp = new RSVP();
                rsvp.AttendeeEmail = User.Identity.Name;

                dinner.RSVPs.Add(rsvp);
                dinnerRepository.Save();
            }

            return Content("Thanks! See you there...");
        }
    }
}
