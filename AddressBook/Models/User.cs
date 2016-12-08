using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AddressBook.Models
{
    public enum Role {Admin=0, Normal=1 };
    public class User : ApplicationUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Role UserType { get; set; }
        public List<Contact> Contacts { get;set; }
    }
}