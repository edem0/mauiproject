namespace Solution.DesktopApp.Views;

public partial class AddJuryView : ContentPage
{
    public AddJudgeViewModel ViewModel => this.BindingContext as AddJudgeViewModel;
    public static string Name => nameof(AddJuryView);

    public AddJuryView(AddJudgeViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
    }
}