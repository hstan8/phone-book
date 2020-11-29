using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using phone_book.Models;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace phone_book.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private static string contact_session = "contactList";

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var phoneBook = new PhoneBook();
            phoneBook.Contacts = checkSession();

            return View(phoneBook);
        }
        public IActionResult SearchContact(string SearchString)
        {
            var phoneBook = new PhoneBook();
            phoneBook.Contacts = checkSession();

            if (!string.IsNullOrEmpty(SearchString))
                phoneBook.Contacts = phoneBook.Contacts.Where(x => x.fullName.Contains(SearchString,System.StringComparison.OrdinalIgnoreCase)).ToList();

            return View("Index", phoneBook);
        }

        public IActionResult AddContact(string FirstName, string LastName, string Number)
        {
            var phoneBook = new PhoneBook();
            phoneBook.Contacts = checkSession();

            phoneBook.Add(new Contact(FirstName, Number, LastName));
            CustomSession.Set(HttpContext.Session, contact_session, phoneBook.Contacts);

            return View("Index", phoneBook);
        }

        public IActionResult RemoveContact(int contactID)
        {
            var phoneBook = new PhoneBook();
            phoneBook.Contacts = checkSession();

            phoneBook.Remove(contactID);
            CustomSession.Set(HttpContext.Session, contact_session, phoneBook.Contacts);
            return View("Index", phoneBook);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private List<Contact> checkSession() 
        {
            var temp = CustomSession.Get<List<Contact>>(HttpContext.Session, contact_session);
            if (temp != null && temp.Any())
                return (temp);
            else
                return (new List<Contact>());
        }
    }
}
