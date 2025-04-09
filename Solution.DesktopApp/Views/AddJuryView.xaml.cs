namespace Solution.DesktopApp.Views;

public partial class AddJuryView : ContentPage
{
    public AddJuryViewModel ViewModel => this.BindingContext as AddJuryViewModel;
    public static string Name => nameof(AddJuryView);

    public AddJuryView(AddJuryViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}