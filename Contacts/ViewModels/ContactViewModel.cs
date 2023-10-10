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
using System.Windows.Input;
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

        
        #region Select Image Using Command
        private ICommand _selectImageCommand;
        private ImageSource _selectImageSource;

        public ICommand SelectImageCommand
        {
            get
            {
                if (_selectImageCommand == null)
                {
                    _selectImageCommand = new Command(ExecuteSelectImageCommand);
                }

                return _selectImageCommand;
            }
        }

        public ImageSource SelectedImageSource
        {
            get { return _selectImageSource; }
            set
            {
                if (_selectImageSource != value)
                {
                    _selectImageSource = value;
                    OnPropertyChanged(nameof(SelectedImageSource));
                }
            }
        }

        private async void ExecuteSelectImageCommand()
        {
            var result = await MediaPicker.PickPhotoAsync();
            if (result != null)
            {
                SelectedImageSource = ImageSource.FromFile(result.FullPath);
            }
        }

        //[RelayCommand]
        //public async Task SelectImage(string ImagePath)
        //{
        //    var result = await MediaPicker.PickPhotoAsync();
        //    if (result != null)
        //    {
        //        var stream = await result.OpenReadAsync();
        //        byte[] imageData;
        //        using (var memoryStream = new MemoryStream())
        //        {
        //            await stream.CopyToAsync(memoryStream);
        //            imageData = memoryStream.ToArray();
        //        }
        //        ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(imageData));
        //        _imagePath = imageSource.ToString();

        //    }
        //}

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


        #region Select Image from gallery or take photo

        //private ICommand _chooseOrTakePhotoCommand;

        //public ICommand ChooseOrTakePhotoCommand
        //{
        //    get
        //    {
        //        if (_chooseOrTakePhotoCommand == null)
        //        {
        //            _chooseOrTakePhotoCommand = new Command(ExecuteChooseOrTakePhotoCommand);
        //        }
        //        return _chooseOrTakePhotoCommand;
        //    }
        //}

        //private async void ExecuteChooseOrTakePhotoCommand()
        //{
        //    try
        //    {
        //        // Display an action sheet to let the user choose between Gallery and Camera
        //        string action = await Application.Current.MainPage.DisplayActionSheet(
        //            "Select or Take Photo",
        //            "Cancel",
        //            null,
        //            "Choose from Gallery",
        //            "Take Photo");

        //        MediaFile photoFile = null;

        //        if (action == "Choose from Gallery")
        //        {
        //            // Pick a photo from the gallery
        //            var result = await MediaPicker.PickPhotoAsync();
        //            if (result != null)
        //            {
        //                photoFile = result;
        //            }
        //        }
        //        else if (action == "Take Photo")
        //        {
        //            // Take a new photo using the device's camera
        //            var request = new MediaCaptureRequest(MediaCaptureOptions.Photo)
        //            {
        //                AllowCropping = false,
        //                CompressionQuality = 90
        //            };
        //            photoFile = await MediaCapture.CaptureAsync(request);
        //        }

        //        // Handle the selected/taken photo (e.g., display it or save it)
        //        if (photoFile != null)
        //        {
        //            // Do something with the photo file (e.g., display it, save it, etc.)
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle any exceptions that might occur during photo selection or capture
        //        Console.WriteLine($"Error: {ex.Message}");
        //    }
        //}

        #endregion
    }
}
