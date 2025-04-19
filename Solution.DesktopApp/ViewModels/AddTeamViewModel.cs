using System.Collections.ObjectModel;
using Solution.Core.Models;
using Solution.Core.Interfaces;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AddTeamViewModel(AppDbContext dbContext, ITeamService teamService) : TeamModel(), IQueryAttributable
{
    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    public IRelayCommand NameIRelayCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand TeamIndexChanged => new RelayCommand(() => this.Members.Validate());

    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }

    private async Task OnSubmitAsync() => await asyncButtonAction();

    [ObservableProperty]
    private ICollection<MemberModel> member;


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(() => LoadMembers());

        bool hasValue = query.TryGetValue("Team", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new team";
            return;
        }

        TeamModel team = result as TeamModel;

        this.Id = team.Id;
        this.Name.Value = team.Name.Value;
        this.Members.Value = team.Members.Value;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update team";
    }


    private async Task OnSaveAsync()
    {
        if(!IsFormValid())
        {
            return;
        }

        var result = await teamService.CreateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Team saved.";

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

        var result = await teamService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Team saved.";
        var title = result.IsError ? "Error" : "Informtaion";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task LoadMembers()
    {
       Member = await dbContext.Members.AsNoTracking()
                                         .OrderBy(x => x.Name)
                                         .Select(x => new MemberModel(x))
                                         .ToListAsync();
    }

    private void ClearForm()
    {
        this.Name.Value = null;
        this.Members.Value = null;
    }

    private bool IsFormValid()
    {
        this.Members.Validate();
        this.Name.Validate();

        return (this.Members?.IsValid ?? false) &&
            this.Name.IsValid;
    }
}
