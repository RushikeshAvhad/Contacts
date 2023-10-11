using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.Models;
using Contacts.UseCases.Interfaces;
using Contacts.Views_MVVM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static SQLite.SQLite3;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.ViewModels
{
    public partial class ContactViewModel : ObservableObject
    {
        private Contact _contact;
        private readonly IViewContactUseCase _viewContactUseCase;
        private readonly IEditContactUseCase _editContactUseCase;
        private readonly IAddContactUseCase _addContactUseCase;

        public Contact Contact
        {
            get => _contact;
            set
            {
                SetProperty(ref _contact, value);
            }
        }

        public bool IsNameProvided { get; set; }
        public bool IsEmailProvided { get; set; }
        public bool IsEmailFormatValid { get; set; }

        public ContactViewModel(
            IViewContactUseCase viewContactUseCase,
            IEditContactUseCase editContactUseCase,
            IAddContactUseCase addContactUseCase)
        {
            Contact = new Contact();
            _viewContactUseCase = viewContactUseCase;
            _editContactUseCase = editContactUseCase;
            _addContactUseCase = addContactUseCase;
        }

        public async Task LoadContact(int contactId)
        {
            Contact = await _viewContactUseCase.ExecuteAsync(contactId);
        }

        [RelayCommand]
        public async Task EditContact()
        {
            if (await ValidateContact())
            {
                await _editContactUseCase.ExecuteAsync(_contact.ContactId, _contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
        }

        [RelayCommand]
        public async Task AddContact()
        {
            if (await ValidateContact())
            {
                await _addContactUseCase.ExecuteAsync(_contact);
                await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
            }
        }

        [RelayCommand]
        public async Task BackToContacts()
        {
            await Shell.Current.GoToAsync($"{nameof(Contacts_MVVM_Page)}");
        }

        
        #region Select Image Using Command & Image Source = Contact.ImagePath

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set
            {
                if (_imagePath != value)
                {
                    _imagePath = value;
                    OnPropertyChanged(nameof(ImagePath));
                } 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand ChangeImageCommand => new Command(async () =>
        {
            await OnSelectImage();
        });

        public async Task OnSelectImage()
        {
            try
            {
                string action = await Application.Current.MainPage.DisplayActionSheet(
                    "Add Photo",
                    "Cancel",
                    null,
                    "Gallery",
                    "Camera");

                FileResult result = null;

                if (action == "Choose from Gallery")
                {
                    result = await MediaPicker.PickPhotoAsync();
                    if (result != null)
                    {
                        Contact.ImagePath = (ImageSource.FromFile(result.FullPath)).ToString();
                    }
                }
                else if (action == "Take Photo")
                {
                    result = await MediaPicker.Default.CapturePhotoAsync();
                    if (result != null)
                    {
                        Contact.ImagePath = (ImageSource.FromFile(result.FullPath)).ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
            }
        }

        #endregion

        private async Task<bool> ValidateContact()
        {
            if (!IsNameProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Name is required", "Ok");
                return false;
            }

            if (!IsEmailProvided)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email is required", "Ok");
                return false;
            }

            if (!IsEmailFormatValid)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Email format is incorrect", "Ok");
                return false;
            }

            return true;
        }
    }
}
