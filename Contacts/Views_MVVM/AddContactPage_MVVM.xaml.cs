using Contacts.ViewModels;

namespace Contacts.Views_MVVM;

public partial class AddContactPage_MVVM : ContentPage
{
    private readonly ContactViewModel _contactViewModel;

    public AddContactPage_MVVM(ContactViewModel contactViewModel)
	{
		InitializeComponent();
        _contactViewModel = contactViewModel;
        BindingContext = _contactViewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        _contactViewModel.Contact = new CoreBusiness.Contact();
    }
}