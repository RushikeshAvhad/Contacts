using Contacts.Models;
using Contacts.UseCases;
using Contacts.UseCases.Interfaces;
using Contact = Contacts.Models.Contact;

namespace Contacts.Views;

[QueryProperty(nameof(ContactId),"Id")]
public partial class EditContactPage : ContentPage
{
    private CoreBusiness.Contact contact;
    private readonly IViewContactUseCase _viewContactUseCase;
    private readonly IEditContactUseCase _editContactUseCase;

    public EditContactPage(
        IViewContactUseCase viewContactUseCase,
        IEditContactUseCase editContactUseCase)
	{
		InitializeComponent();
        _viewContactUseCase = viewContactUseCase;
        _editContactUseCase = editContactUseCase;
    }

    private void btnCancel_Clicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("..");
    }

    public string ContactId
    {
        set
        {
            contact = _viewContactUseCase.ExecuteAsync(int.Parse(value)).GetAwaiter().GetResult();

            if (contact != null)
            {
                contactCtrl.Name = contact.Name;
                contactCtrl.Address = contact.Address;
                contactCtrl.Email = contact.Email;
                contactCtrl.Phone = contact.Phone;
            }
        }
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        contact.Name = contactCtrl.Name;
        contact.Address = contactCtrl.Address;
        contact.Email = contactCtrl.Email;
        contact.Phone = contactCtrl.Phone;

        //ContactRepository.UpdateContact(contact.ContactId, contact);
        await _editContactUseCase.ExecuteAsync(contact.ContactId, contact);
        await Shell.Current.GoToAsync("..");
    }

    private void contactCtrl_OnError(object sender, string e)
    {
        DisplayAlert("Error", e, "Ok");
    }
}