using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Contacts.UseCases.Interfaces;
using Contacts.Views_MVVM;
using System.ComponentModel;
using System.Windows.Input;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.ViewModels
{
    public partial class ContactViewModel : ObservableObject, INotifyPropertyChanged
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
            //Contact = new Contact();
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

                if (action == "Gallery")
                {
                    result = await MediaPicker.PickPhotoAsync();
                    if (result != null)
                    {
                        string filePath = Convert.ToString(ImageSource.FromFile(result.FullPath));
                        string textValue = filePath.Replace("File: ", null);
                        Contact.ImagePath = textValue;
                    }
                }
                else if (action == "Camera")
                {
                    result = await MediaPicker.Default.CapturePhotoAsync();
                    if (result != null)
                    {
                        string filePath = (ImageSource.FromFile(result.FullPath)).ToString();
                        string textValue = filePath.Replace("File: ", null);
                        Contact.ImagePath = textValue;
                    }
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
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
