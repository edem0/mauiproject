namespace Solution.DesktopApp.Views;

public partial class CompetitionListView : ContentPage
{
	public CompetitionListViewModel ViewModel => this.BindingContext as CompetitionListViewModel;
	public static string Name => nameof(CompetitionListView);
	
	public CompetitionListView(CompetitionListViewModel viewModel)
	{
		BindingContext = viewModel;
		InitializeComponent();
	}
}