using TakeNotes.ViewModels;

namespace TakeNotes.Views;

public partial class NoteView : ContentPage
{
	public NoteView()
	{
		InitializeComponent();
        BindingContext = new NoteViewModel();

    }
}