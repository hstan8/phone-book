using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace phone_book.Models
{
    public class Contact
    {
        public Contact() : base() { }
        public Contact(string inputFirstName, string inputNumber, string inputLastName = "")
        {
            firstName = inputFirstName;
            lastName = inputLastName;
            fullName = String.Join(' ',inputFirstName, inputLastName);
            phoneNumber = inputNumber;
        }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string fullName { get; set; }

        public string phoneNumber { get; set; }

        public int ID { get; set; }
    }

    public class PhoneBook
    {
        public List<Contact> Contacts { get; set; }

        public PhoneBook()
        {
            Contacts = new List<Contact>();
        }

        public void Add(Contact contact) 
        {
            var maxId = Contacts.Any() ? Contacts.Select(x => x.ID).Max() : 0;
            contact.ID = maxId + 1;
            Contacts.Add(contact);
        }

        public void Remove(int contactID) 
        {
            Contacts.RemoveAll(x => x.ID == contactID);
        }
    }
}
