using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);
    public IAsyncRelayCommand AddNewCompetitionCommand => new AsyncRelayCommand(OnAddNewCompetitionAsync);
    public IAsyncRelayCommand ListAllCompetitionsCommand => new AsyncRelayCommand(OnListAllCompetitionsAsync);


    private async Task OnExitAsync() => Application.Current.Quit();

    private async Task OnAddNewCompetitionAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(CreateOrEditCompetitionView));
    }

    private async Task OnListAllCompetitionsAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(CompetitionListView));
    }
}
