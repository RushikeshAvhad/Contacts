using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Models;
using Contacts.UseCases.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact _contact;
        private readonly IViewContactUseCase _viewContactUseCase;

        public Contact Contact
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public ContactViewModel(IViewContactUseCase viewContactUseCase)
        {
            Contact = new Contact();
            _viewContactUseCase = viewContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await _viewContactUseCase.ExecuteAsync(contactId);
        }

        //[RelayCommand]
        //public void SaveContact()
        //{
        //    ContactRepository.UpdateContact(this.Contact.ContactId, this.Contact);
        //}
    }
}
