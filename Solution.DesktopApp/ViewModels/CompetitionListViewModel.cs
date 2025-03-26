using System.Collections.ObjectModel;
using System.Windows.Input;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

public class CompetitionListViewModel
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }

    private ObservableCollection<CompetitionModel> competitions;

    private int page = 1;
    private bool isLoading = false;
    private bool hasNextPage = false;
    private int numberOfMotorcyclesInDB = 0;


    private async Task OnAppearingAsync() 
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasNextPage);

        //await LoadCompetitionsAsync();
    }

    private async Task OnDisappearingAsync() { }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        //await LoadCompetitionsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        //await LoadCompetitionsAsync();
    }
}
