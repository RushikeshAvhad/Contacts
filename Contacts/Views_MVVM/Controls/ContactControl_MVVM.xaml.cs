using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Contacts.Views_MVVM.Controls;

public partial class ContactControl_MVVM : ContentPage
{
	public bool IsForEdit { get; set; }
	public bool IsForAdd { get; set; }
	public ContactControl_MVVM()
	{
		InitializeComponent();
	}

    protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        base.OnPropertyChanged(propertyName);

		if (IsForAdd && !IsForEdit)
			btnSave.SetBinding(Button.CommandProperty, "AddContactCommand");
		else if (!IsForAdd && IsForEdit)
			btnSave.SetBinding(Button.CommandProperty, "EditContactCommand");
    }

    private void EmailEntry_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        string email = e.NewTextValue;

        bool isValid = IsEmailValid(email);

        if (isValid)
        {
            emailEntry.TextColor = Color.FromRgb(0, 255, 0);
        }
        else
        {
            emailEntry.TextColor = Color.FromRgb(255, 0, 0);
        }
    }


    private bool IsEmailValid(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        string emailPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
        Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

        return regex.IsMatch(email);
    }

    private void MobileEntry_OnTextChanged(object sender, TextChangedEventArgs e)
    {
        // MobileEntry
        string mobileNumber = e.NewTextValue;

        bool isValid = IsMobileNumberValid(mobileNumber);

        if (isValid)
        {
            MobileEntry.TextColor = Color.FromRgb(0, 255, 0);
        }
        else
        {
            MobileEntry.TextColor = Color.FromRgb(255, 0, 0);
        }
    }

    private bool IsMobileNumberValid(string mobileNumber)
    {
        if (string.IsNullOrWhiteSpace((mobileNumber)))
            return false;

        string mobilePattern = @"^[0-9]{10}$";
        Regex regex = new Regex(mobilePattern);

        return regex.IsMatch(mobileNumber);
    }

    //public async void OnSelectImageClicked(object sender, EventArgs e)
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
    //        photo.Source = imageSource;
    //    }
    //}
}