using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.UseCases.Interfaces;
using Contacts.Views_MVVM;
using System.Collections.ObjectModel;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.ViewModels
{
    public partial class ContactsViewModel : ObservableObject
    {
        private readonly IViewContactsUseCase _viewContactsUseCase;
        private readonly IDeleteContactUseCase _deleteContactUseCase;

        public ObservableCollection<Contact> Contacts { get; set; }

        private string filterText;

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                LoadContactsAsync(filterText);
            }
        }


        public ContactsViewModel(
            IViewContactsUseCase viewContactsUseCase,
            IDeleteContactUseCase deleteContactUseCase)
        {
            _viewContactsUseCase = viewContactsUseCase;
            _deleteContactUseCase = deleteContactUseCase;
            Contacts = new ObservableCollection<Contact>();
        }

        public async Task LoadContactsAsync(string filterText = null)
        {
            Contacts.Clear();

            var contacts = await _viewContactsUseCase.ExecuteAsync(filterText);
            if (contacts != null && contacts.Count > 0)
            {
                foreach (var contact in contacts)
                {
                    Contacts.Add(contact);
                }
            }
        }

        [RelayCommand]
        public async Task DeleteContact(int contactId)
        {
            bool deleteConfirmed = await Application.Current.MainPage.DisplayAlert("Delete Confirmation", "Are you sure you want to delete this contact?", "Yes", "No");
            if (deleteConfirmed)
            {
                await _deleteContactUseCase.ExecuteAsync(contactId);
                await LoadContactsAsync();
            }
            else
            {
                await LoadContactsAsync();
            }
        }

        [RelayCommand]
        public async Task GotoEditContact(int contactId)
        {
            await Shell.Current.GoToAsync($"{nameof(EditContactPage_MVVM)}?Id={contactId}");
        }

        [RelayCommand]
        public async Task GotoAddContact()
        {
            await Shell.Current.GoToAsync(nameof(AddContactPage_MVVM));
        }


        #region Press 1 Second On Frame to redirect to Call Or Mail

        private DateTime _tapStartTime;

        [RelayCommand]
        public async Task TappedFrame(Contact contact)
        {
            if (_tapStartTime == DateTime.MinValue)
            {
                _tapStartTime = DateTime.Now;
                return;
            }

            var duration = DateTime.Now - _tapStartTime;
            if (duration.TotalMilliseconds >= 1000)
            {
                var name = contact.Name;
                string action = await Application.Current.MainPage.DisplayActionSheet(
                    $"{name}",
                    "Cancel",
                    null,
                    "Call",
                    "Mail");


                if (action == "Call")
                {
                    var phoneNumber = contact.Phone;
                    try
                    {
                        await Launcher.OpenAsync(new Uri($"tel:{phoneNumber}"));
                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"{e}", "OK");
                    }
                    await LoadContactsAsync();
                }
                else if (action == "Mail")
                {
                    try
                    {
                        await Launcher.OpenAsync(new Uri($"mailto:{contact.Email}"));
                    }
                    catch (Exception e)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", $"{e}", "OK");
                    }
                    await LoadContactsAsync();
                }
            }

            _tapStartTime = DateTime.MinValue;
        }

        #endregion
    }
}
