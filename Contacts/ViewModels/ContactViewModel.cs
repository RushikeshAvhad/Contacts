using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.Models.Contact;

namespace Contacts.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        public Contact Contact { get; set; }

        public ContactViewModel()
        {
            Contact = ContactRepository.GetContactById(1);
        }

        [RelayCommand]
        public void SaveContact()
        {
            ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
        }
    }
}
