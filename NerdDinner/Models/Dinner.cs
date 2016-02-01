using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace NerdDinner.Models
{
    public class Dinner
    {
        public int DinnerID { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [RegularExpression(".+\\@.+\\..+", ErrorMessage = "enter a valid email address")]
        public string HostedBy { get; set; }

        [Required]
        public string Country { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public virtual ICollection<RSVP> RSVPs { get; set; }

        public bool IsValid()
        {
            return !GetRuleViolations().Any();
        }

        public IEnumerable<RuleViolation> GetRuleViolations()
        {
            if (String.IsNullOrEmpty(Country))
            {
                yield return new RuleViolation("Field value test is required", "Country");
            }
        }

        public bool IsHostedBy(string userName)
        {
            return HostedBy.Equals(userName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}