using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace StopNShop2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        public string Street { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }
    }
}
