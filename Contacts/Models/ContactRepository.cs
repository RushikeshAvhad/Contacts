using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contacts.Models
{
    public static class ContactRepository
    {
        public static List<Contact> _contacts = new List<Contact>()
        {
            new Contact {Name = "John Doe", Email="johnDoe@gmail.com"},
            new Contact {Name = "Jane Doe", Email="janeDoe@gmail.com"},
            new Contact {Name = "Tom Hanks", Email="tomHanks@gmail.com"},
            new Contact {Name = "Frank Liu", Email="frankliu@gmail.com"}
        };

        public static List<Contact> GetContacts() => _contacts;

        public static Contact GetContactById(int contactId)
        {
            return _contacts.FirstOrDefault(x => x.ContactId == contactId);
        }
    }
}
