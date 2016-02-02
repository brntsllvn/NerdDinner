using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace NerdDinner.Models
{
    public class User : IdentityUser
    {
        public string Email { get; set; }
    }
}