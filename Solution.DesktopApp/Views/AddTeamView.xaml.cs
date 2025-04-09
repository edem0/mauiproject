namespace Solution.DesktopApp.Views;

public partial class AddTeamView : ContentPage
{
    public AddTeamViewModel ViewModel => this.BindingContext as AddTeamViewModel;
    public static string Name => nameof(AddTeamView);

    public AddTeamView(AddTeamViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}