using Microsoft.EntityFrameworkCore;
using Solution.Core.Interfaces;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class CreateOrEditCompetitionViewModel(AppDbContext dbContext, ICompetitionService competitionService) : CompetitionModel(), IQueryAttributable
{
    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    public IRelayCommand NameIRelayCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand DateIRelayCommand => new RelayCommand(() => this.Date.Validate());

    public IRelayCommand JuryIndexChangedCommand => new RelayCommand(() => this.Jury.Validate());

    public IRelayCommand TeamsIndexChangedCommand => new RelayCommand(() => this.Teams.Validate());

    public IRelayCommand LocationIndexChangedCommand => new RelayCommand(() => this.Date.Validate());

    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }
    private async Task OnSubmitAsync() => await asyncButtonAction();

    [ObservableProperty]
    private ICollection<JuryModel> jur;

    [ObservableProperty]
    private ICollection<TeamModel> team;

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(() => LoadJury());
        await Task.Run(() => LoadTeams());

        bool hasValue = query.TryGetValue("Competition", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new competition";
            return;
        }

        CompetitionModel competition = result as CompetitionModel;

        this.Id = competition.Id;
        this.Name.Value = competition.Name.Value;
        this.Date.Value = competition.Date.Value;
        this.Location.Value = competition.Location.Value;
        this.Jury = competition.Jury;
        this.Teams = competition.Teams;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update competition";
    }


    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await competitionService.CreateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Competition saved.";

        var title = result.IsError ? "Error" : "Informtaion";

        if (result.IsError)
        {
            ClearForm();
        }

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnUpdateAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await competitionService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Competition saved.";
        var title = result.IsError ? "Error" : "Informtaion";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task LoadJury()
    {
        Jur = await dbContext.Jury.AsNoTracking()
                                   .OrderBy(x => x.Name)
                                   .Select(x => new JuryModel(x))
                                   .ToListAsync();
    }

    private async Task LoadTeams()
    {
        Team = await dbContext.Teams.AsNoTracking()
                                   .OrderBy(x => x.Name)
                                   .Select(x => new TeamModel(x))
                                   .ToListAsync();
    }

    private void ClearForm()
    {
        this.Jury.Value = null;
        this.Teams.Value = null;
        this.Name.Value = null;
        this.Date.Value = DateTime.Now;
        this.Location.Value = null;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.Date.Validate();
        this.Location.Validate();
        this.Jury.Validate();
        this.Teams.Validate();

        return this.Name.IsValid &&
            this.Date.IsValid &&
            this.Location.IsValid &&
            (this.Jury?.IsValid ?? false) &&
            (this.Teams?.IsValid ?? false);

    }


}
