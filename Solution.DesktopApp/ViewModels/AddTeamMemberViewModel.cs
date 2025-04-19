
namespace Solution.DesktopApp.ViewModels;

public class AddTeamMemberViewModel
{
    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }

    private async Task OnSubmitAsync()
    {
        throw new NotImplementedException();
    }

    private async Task OnImageSelectAsync()
    {
        throw new NotImplementedException();
    }
}
