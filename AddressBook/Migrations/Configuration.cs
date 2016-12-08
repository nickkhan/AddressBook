namespace AddressBook.Migrations
{
    using Microsoft.AspNet.Identity;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AddressBook.Models.AddressBookContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;

        }

        protected override void Seed(AddressBook.Models.AddressBookContext context)
        {
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("password");
            context.Users.AddOrUpdate(              
              new User {Username="nickkhandev@gmail.com", FirstName = "Nick", LastName="Khan", UserType = Models.Role.Admin, Password=password, Contacts = new List<Contact>()
              {
                  new Contact() {
                    Id = Guid.NewGuid(),
                    FirstName = "Simon",
                    LastName = "Fraser",
                    City = "Mississauga",
                    Province = "ON",
                    PhoneNumber = "647-999-9999",
                    PostalCode = "L5C1G5",
                    Country = "CA",
                    StreetName = "64 blairholm ave"
                  },
                  new Contact()
                  {
                    Id = Guid.NewGuid(),
                    FirstName = "Judy",
                    LastName = "Robertson",
                    City = "Sudbury",
                    Province = "ON",
                    PhoneNumber = "647-999-9999",
                    PostalCode = "P3E5M4",
                    Country = "CA",
                    StreetName = "885 Regent St"
                  }
              },

              },
              new User {Username="fawad.khan@hotmail.ca", FirstName= "Fawad",LastName="Khan", UserType = Models.Role.Normal, Password=password, Contacts = new List<Contact>()
              {
                  new Contact()
                  {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName ="Doe",
                    City= "North York",
                    Province="ON",
                    PhoneNumber = "647-999-9999",
                    PostalCode = "M2N2M2",
                    Country ="CA",
                    StreetName ="20 Yonge St"
                  },
                  new Contact()
                  {
                    Id = Guid.NewGuid(),
                    FirstName = "John",
                    LastName = "Doe",
                    City = "Winnipeg",
                    Province = "MB",
                    PhoneNumber = "647-999-9999",
                    PostalCode = "R3T2S2",
                    Country = "CA",
                    StreetName = "631 Grierson ave"
                  }
              }
              },
              new User {Username = "fkhan.dev@live.com", FirstName="Faz", LastName="Khan", UserType = Models.Role.Normal, Password=password, Contacts= new List<Contact>()}
            );


        }
    }
}
