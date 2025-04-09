namespace Solution.DesktopApp.Views;

public partial class AddTeamMemberView : ContentPage
{
    public AddTeamMemberViewModel ViewModel => this.BindingContext as AddTeamMemberViewModel;
    public static string Name => nameof(AddTeamMemberView);

    public AddTeamMemberView(AddTeamMemberViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}