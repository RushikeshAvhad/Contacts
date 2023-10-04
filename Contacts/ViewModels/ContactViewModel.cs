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
        private Contact _contact;
        public Contact Contact
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public ContactViewModel()
        {
            Contact = new Contact();
        }

        public void LoadContact(int contactId)
        {
            Contact = ContactRepository.GetContactById(contactId);
        }

        [RelayCommand]
        public void SaveContact()
        {
            ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
        }
    }
}
