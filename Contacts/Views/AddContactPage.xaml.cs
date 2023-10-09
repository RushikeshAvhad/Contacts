using Contacts.Models;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.CoreBusiness.Contact;

namespace Contacts.Views;

public partial class AddContactPage : ContentPage
{
    private readonly IAddContactUseCase _addContactUseCase;

    public AddContactPage(IAddContactUseCase addContactUseCase)
    {
        InitializeComponent();
        _addContactUseCase = addContactUseCase;
    }

    private async void contactCtrl_OnSave(object sender, EventArgs e)
    {
        await _addContactUseCase.ExecuteAsync(new Contact
        {
            Name = contactCtrl.Name,
            Email = contactCtrl.Email,
            Address = contactCtrl.Address,
            Phone = contactCtrl.Phone
            //ImagePath = contactCtrl.ImagePath
        });

        await Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnCancel(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "Ok");
    }
}