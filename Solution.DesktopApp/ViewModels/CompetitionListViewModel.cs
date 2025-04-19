using System.Collections.ObjectModel;
using System.Windows.Input;
using Solution.Core.Interfaces;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CompetitionListViewModel(ICompetitionService competitionService)
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public ICommand PreviousPageCommand { get; private set; }
    public ICommand NextPageCommand { get; private set; }

    [ObservableProperty]
    private ObservableCollection<CompetitionModel> competitions;

    private int page = 1;
    private bool isLoading = false;
    private bool hasMaxPage = false;
    private int numberOfCompetitionsInDB = 0;


    private async Task OnAppearingAsync() 
    {
        PreviousPageCommand = new Command(async () => await OnPreviousPageAsync(), () => page > 1 && !isLoading);
        NextPageCommand = new Command(async () => await OnNextPageAsync(), () => !isLoading && hasMaxPage);

        await LoadCompetitionsAsync();
    }

    private async Task OnPreviousPageAsync()
    {
        if (isLoading) return;

        page = page <= 1 ? 1 : --page;
        await LoadCompetitionsAsync();
    }

    private async Task OnNextPageAsync()
    {
        if (isLoading) return;

        page++;
        await LoadCompetitionsAsync();
    }

    private async Task LoadCompetitionsAsync()
    {
        isLoading = true;
        
        var result = await competitionService.GetPagedAsync(page);

        if(result.IsError)
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Competitions not loaded!", "OK");
            return;
        }

        Competitions = new ObservableCollection<CompetitionModel>(result.Value.Items);
        numberOfCompetitionsInDB = result.Value.Count;

        hasMaxPage = numberOfCompetitionsInDB - (page * 10) > 0;
        isLoading = false;

        ((Command)PreviousPageCommand).ChangeCanExecute();
        ((Command)NextPageCommand).ChangeCanExecute();
    }
}
