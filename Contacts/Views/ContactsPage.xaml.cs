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

    private async void listContacts_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
		if (listContacts.SelectedItem != null)
		{
			await Shell.Current.GoToAsync(nameof(EditContactPage));
		}
    }

    private void listContacts_ItemTapped(object sender, ItemTappedEventArgs e)
    {
		/* When we click on item ItemSelected event triggers
		 * If we click on selected item ItemSelected event will not trigger again & again 
		 * but ItemTapped event trigger every time we tap on Item */
    }
}