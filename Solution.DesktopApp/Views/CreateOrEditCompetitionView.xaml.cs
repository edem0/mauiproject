namespace Solution.DesktopApp.Views;

public partial class CreateOrEditCompetitionView : ContentPage
{
	public CreateOrEditCompetitionViewModel ViewModel => this.BindingContext as CreateOrEditCompetitionViewModel;
	public static string Name => nameof(CreateOrEditCompetitionView);

	public CreateOrEditCompetitionView(CreateOrEditCompetitionViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}