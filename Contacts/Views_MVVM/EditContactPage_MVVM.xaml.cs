using Contacts.ViewModels;

namespace Contacts.Views_MVVM;


[QueryProperty(nameof(ContactId), "Id")]
public partial class EditContactPage_MVVM : ContentPage
{
    private readonly ContactViewModel _contactViewModel;

    public EditContactPage_MVVM(ContactViewModel contactViewModel)
	{
		InitializeComponent();
        _contactViewModel = contactViewModel;

        BindingContext = _contactViewModel;
    }

    public string ContactId
    {
        set
        {
            if (!string.IsNullOrWhiteSpace(value) && int.TryParse(value, out int contactId))
            {
                LoadContact(contactId);
            }
        }
    }

    private async void LoadContact(int contactId)
    {
        await _contactViewModel.LoadContact(contactId);
    }
}