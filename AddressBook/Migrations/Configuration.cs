namespace AddressBook.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Claims;

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
            var rolestore = new RoleStore<IdentityRole>(context);
            var rolemanager = new RoleManager<IdentityRole>(rolestore);

            var userstore = new UserStore<ApplicationUser>(context);
            var usermanager = new UserManager<ApplicationUser>(userstore);

            if (!context.Roles.Any(r=>r.Name == "Admin"))
            {
               
                var role = new IdentityRole()
                {
                    Name = "Admin"
                };
                rolemanager.Create(role);                
            }

            if (!context.Roles.Any(r => r.Name == "Normal"))
            {
                var role = new IdentityRole()
                {
                    Name = "Normal"
                };
                rolemanager.Create(role);
            }

            
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("password");

            context.Users.AddOrUpdate(              
              new ApplicationUser
              {
                  Email= "nickkhandev@gmail.com", EmailConfirmed=true, UserName="nickkhandev@gmail.com", FirstName = "Nick", LastName="Khan",  PasswordHash=password, SecurityStamp = Guid.NewGuid().ToString(), Contacts = new List<Contact>()
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
                    Street = "64 blairholm ave"
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
                    Street = "885 Regent St"
                  }
              },

              },
              new ApplicationUser
              {
                  Email= "fawad.khan@hotmail.ca", UserName = "fawad.khan@hotmail.ca", FirstName= "Fawad",LastName="Khan", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString(),
                  Contacts = new List<Contact>()
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
                        Street ="20 Yonge St"
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
                        Street = "631 Grierson ave"
                      }
                  }
              },
              new ApplicationUser { Email= "fkhan.dev@live.com", UserName = "fkhan.dev@live.com", FirstName="Faz", LastName="Khan", PasswordHash = password, SecurityStamp = Guid.NewGuid().ToString(), Contacts = new List<Contact>()}
            );
            context.SaveChanges();

            // Add Users to Roles
            usermanager.AddToRole(context.Users.Where(u => u.UserName == "nickkhandev@gmail.com").FirstOrDefault().Id, "Admin");
            usermanager.AddClaim(context.Users.Where(u => u.UserName == "nickkhandev@gmail.com").FirstOrDefault().Id, new System.Security.Claims.Claim(ClaimTypes.Role, "Admin"));

            usermanager.AddToRole(context.Users.Where(u => u.UserName == "fawad.khan@hotmail.ca").FirstOrDefault().Id, "Normal");
            usermanager.AddClaim(context.Users.Where(u => u.UserName == "fawad.khan@hotmail.ca").FirstOrDefault().Id, new System.Security.Claims.Claim(ClaimTypes.Role, "Normal"));

            usermanager.AddToRole(context.Users.Where(u => u.UserName == "fkhan.dev@live.com").FirstOrDefault().Id, "Normal");
            usermanager.AddClaim(context.Users.Where(u => u.UserName == "fkhan.dev@live.com").FirstOrDefault().Id, new System.Security.Claims.Claim(ClaimTypes.Role, "Normal"));            
            
        }
    }
}
