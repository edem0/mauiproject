using System.Collections.ObjectModel;
using Solution.Core.Models;

namespace Solution.DesktopApp.ViewModels;

public class AddTeamViewModel
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }

    private ObservableCollection<MemberModel> TeamMembers;
}
