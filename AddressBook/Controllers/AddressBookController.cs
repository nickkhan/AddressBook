using AddressBook.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace AddressBook.Controllers
{
    [Authorize]
    public class AddressBookController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;


        public AddressBookController()
        {

        }

        public AddressBookController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        // GET: AddressBook
        public async Task<ActionResult> Index(AddressBookMessageId? message)
        {
            ViewBag.StatusMessage =
                message == AddressBookMessageId.AddContactSuccess ? "Your contact has been added."
                : message == AddressBookMessageId.DeleteContactSuccess? "Your contact has been delete."
                : message == AddressBookMessageId.EditContactSuccess? "your contact has been updated."
                : message == AddressBookMessageId.Error? "an error occured while processing your request."
                : "";

            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if (user != null)
            {
                var model = new AddressBookViewModel();
                model.userId = user.Id;
                model.UserName = user.UserName;
                model.contacts = user.Contacts.ToList();
                model.Role = user.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;
                ViewBag.Role = model.Role;
                return View(model);
            }

            return View("Error");
        }

        //
        // GET: /AddressBook/EditUser
        public ActionResult EditContact(string id)
        {
            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var contact = appuser.Contacts.Where(c => c.Id.ToString() == id).FirstOrDefault();
            if (contact != null)
            {
                var model = new ContactViewModel
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Street = contact.Street,
                    PostalCode = contact.PostalCode,
                    Province = contact.Province,
                    City = contact.City,
                    Country = contact.Country
                    
                };
                return View(model);
            }

            return View("Error");
        }

        //
        // POST: /AddressBook/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditContact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var user = await UserManager.FindByIdAsync(userId);
            var contact = user.Contacts.Where(c => c.Id.ToString() == model.Id).FirstOrDefault();

            if (contact != null)
            {
                contact.FirstName = model.FirstName;
                contact.LastName = model.LastName;
                contact.PhoneNumber = model.PhoneNumber;
                contact.PostalCode = model.PostalCode;
                contact.Province = model.Province;
                contact.Street = model.Street;
                contact.Country = model.Country;
                contact.City = model.City;
            }

            
            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index", "AddressBook", new { Message = AddressBookMessageId.EditContactSuccess });

            return View("Error");
        }

        //
        // GET: /AddressBook/EditUser
        public ActionResult DeleteContact(string id)
        {
            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var contact = appuser.Contacts.Where(c => c.Id.ToString() == id).FirstOrDefault();
            if (contact != null)
            {
                var model = new ContactViewModel
                {
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    PhoneNumber = contact.PhoneNumber,
                    Street = contact.Street,
                    PostalCode = contact.PostalCode,
                    Province = contact.Province,
                    City = contact.City,
                    Country = contact.Country

                };
                return View(model);
            }

            return View("Error");
        }

        //
        // POST: /AddressBook/EditUser
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteContact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var user = await UserManager.FindByIdAsync(userId);
            var contact = user.Contacts.Where(c => c.Id.ToString() == model.Id).FirstOrDefault();

            user.Contacts.Remove(contact);

            var result = await UserManager.UpdateAsync(user);

            if (result.Succeeded)
                return RedirectToAction("Index", "AddressBook", new { Message = AddressBookMessageId.DeleteContactSuccess });

            return View("Error");
        }
        //
        // GET: /AddressBook/AddContact
        public ActionResult AddContact()
        {
            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var model = new ContactViewModel();

            return View(model);
        }

        //
        // POST: /AddressBook/AddContact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddContact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userId = User.Identity.GetUserId();
            var appuser = UserManager.FindById(userId);
            ViewBag.Role = appuser.Claims.Where(e => e.ClaimType == ClaimTypes.Role).FirstOrDefault().ClaimValue;

            var contact = new Contact()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                PostalCode = model.PostalCode,
                Province = model.Province,
                Street = model.Street,
                Country = model.Country,
                City = model.City
            };

            appuser.Contacts.Add(contact);

            var result = await UserManager.UpdateAsync(appuser);
            if (result.Succeeded)
                return RedirectToAction("Index", "AddressBook", new { Message = AddressBookMessageId.AddContactSuccess });

            return View("Error");
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public enum AddressBookMessageId
        {
            AddContactSuccess,
            EditContactSuccess,
            DeleteContactSuccess,
            Error
        }
    }
}