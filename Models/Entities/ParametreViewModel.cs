using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CentralisationV0.Models.Entities
{
    public class ParametreViewModel
    {
        public RegisterViewModel RegisterViewModel { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public Dictionary<string, string> UserRoles { get; set; }
    }
}