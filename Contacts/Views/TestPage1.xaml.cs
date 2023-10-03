using Contacts.ViewModels;

namespace Contacts.Views;

public partial class TestPage1 : ContentPage
{
	private ContactViewModel _viewModel;

	public TestPage1()
	{
		InitializeComponent();
		_viewModel = new ContactViewModel();
		BindingContext = _viewModel;
	}
}