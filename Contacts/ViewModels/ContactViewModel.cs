using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Models;
using Contacts.UseCases.Interfaces;
using Contacts.Views_MVVM;
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
        private readonly IEditContactUseCase _editContactUseCase;

        public Contact Contact
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public ContactViewModel(IViewContactUseCase viewContactUseCase, IEditContactUseCase editContactUseCase)
        {
            Contact = new Contact();
            _viewContactUseCase = viewContactUseCase;
            _editContactUseCase = editContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await _viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async Task EditContact()
        {
            await _editContactUseCase.ExecuteAsync(_contact.ContactId, _contact);
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }
    }
}
