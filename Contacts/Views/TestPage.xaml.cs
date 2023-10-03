using Contacts.Models;
using Contact = Contacts.Models.Contact;

namespace Contacts.Views;

public partial class TestPage : ContentPage
{
	private Contact _contact;
	public TestPage()
	{
		InitializeComponent();		
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();

		_contact = ContactRepository.GetContactById(1);
		entryName.Text = _contact.Name;
		entryPhone.Text = _contact.Phone;
		entryEmail.Text = _contact.Email;
		entryAddress.Text = _contact.Address;
    }

    private void btnSave_Clicked(object sender, EventArgs e)
    {
		_contact.Name = entryName.Text;
		_contact.Address = entryAddress.Text;
		_contact.Email = entryEmail.Text;
		_contact.Phone = entryPhone.Text;

		ContactRepository.UpdateContact(_contact.ContactId, _contact);
    }
}