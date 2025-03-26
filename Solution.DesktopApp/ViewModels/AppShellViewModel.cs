using Solution.DesktopApp.Views;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AppShellViewModel
{
    public IAsyncRelayCommand ExitCommand => new AsyncRelayCommand(OnExitAsync);
    public IAsyncRelayCommand AddNewCompetitionCommand => new AsyncRelayCommand(OnAddNewCompetitionAsync);
    public IAsyncRelayCommand ListAllCompetitionsCommand => new AsyncRelayCommand(OnListAllCompetitionsAsync);
    public IAsyncRelayCommand AddJuryCommand => new AsyncRelayCommand(OnAddJuryAsync);
    public IAsyncRelayCommand AddTeamCommand => new AsyncRelayCommand(OnAddTeamAsync);
    public IAsyncRelayCommand AddTeamMemberCommand => new AsyncRelayCommand(OnAddTeamMemberAsync);


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

    private async Task OnAddJuryAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(AddJuryView));
    }

    private async Task OnAddTeamAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(AddTeamView));
    }

    private async Task OnAddTeamMemberAsync()
    {
        Shell.Current.ClearNavigationStack();
        await Shell.Current.GoToAsync(nameof(AddTeamMemberView));
    }
}
