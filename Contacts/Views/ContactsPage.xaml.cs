namespace Contacts.Views;

public partial class ContactsPage : ContentPage
{
	public ContactsPage()
	{
		InitializeComponent();

		List<Contact> contacts = new List<Contact>()
		{
			new Contact {Name = "John Doe", Email="johnDoe@gmail.com"},
			new Contact {Name = "Jane Doe", Email="janeDoe@gmail.com"},
			new Contact {Name = "Tom Hanks", Email="tomHanks@gmail.com"},
			new Contact {Name = "Frank Liu", Email="frankliu@gmail.com"}
		};

		listContacts.ItemsSource = contacts;
	}

	public class Contact
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}

    private void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		//	Logic

		DisplayAlert("Message", "Item Selected Event in MAUI List View", "OK");

		listContacts.SelectedItem = null;	// Will not focus on selected Item.
    }
}