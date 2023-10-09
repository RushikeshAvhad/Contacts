using System.Text.RegularExpressions;

namespace Contacts.Views.Controls;

public partial class ContactControl : ContentView
{
    public event EventHandler<string> OnError;
    public event EventHandler<EventArgs> OnSave;
    public event EventHandler<EventArgs> OnCancel;

	public ContactControl()
	{
		InitializeComponent();
	}

	public string Name
	{
		get
		{
			return entryName.Text;
		}
		set
		{
			entryName.Text = value;
		}
	}

    public string Email
    {
        get
        {
            return entryEmail.Text;
        }
        set
        {
            entryEmail.Text = value;
        }
    }

    public string Address
    {
        get
        {
            return entryAddress.Text;
        }
        set
        {
            entryAddress.Text = value;
        }
    }

    public string Phone
    {
        get
        {
            return entryPhone.Text;
        }
        set
        {
            entryPhone.Text = value;
        }
    }

    //public string ImagePath
    //{
    //    get
    //    {
    //        return entryPhone.Text;
    //    }
    //    set
    //    {
    //        entryPhone.Text = value;
    //    }
    //}

    private void btnSave_Clicked(object sender, EventArgs e)
    {
        if (nameValidator.IsNotValid)
        {
            OnError?.Invoke(sender, "Name is required.");
            return;
        }

        if (emailValidator.IsNotValid)
        {
            foreach (var error in emailValidator.Errors)
            {
                entryEmail.TextColor = Color.FromRgb(255, 0, 0); // Red color
                OnError?.Invoke(sender, error.ToString());
            }
            return;
        }

        OnSave?.Invoke(sender, e);
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        OnCancel?.Invoke(sender, e);
    }

    //private void EntryEmail_OnTextChanged(object sender, TextChangedEventArgs e)
    //{
    //    string email = e.NewTextValue;

    //    bool isValid = IsEmailValid(email);

    //    if (isValid)
    //    {
    //        entryEmail.TextColor = Color.FromRgb(0, 255, 0);
    //    }
    //    else
    //    {
    //        entryEmail.TextColor = Color.FromRgb(255, 0, 0);
    //    }
    //}

    //// Regular expression for basic email validation.
    //private bool IsEmailValid(string email)
    //{
    //    if (string.IsNullOrWhiteSpace(email))
    //        return false;

    //    string emailPattern = @"^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$";
    //    Regex regex = new Regex(emailPattern, RegexOptions.IgnoreCase);

    //    return regex.IsMatch(email);
    //}
}