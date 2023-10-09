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
}