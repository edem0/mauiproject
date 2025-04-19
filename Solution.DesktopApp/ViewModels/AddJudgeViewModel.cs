using System.Threading.Tasks;
using Solution.Core.Models;
using Solution.ValidationLibrary;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Solution.Core.Interfaces;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AddJudgeViewModel(AppDbContext dbContext, IJudgeService judgeService, IGoogleDriveService googleDriveService) : JuryModel(), IQueryAttributable
{
    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
    public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);

    public IRelayCommand NameIRelayCommand => new RelayCommand(() => this.Name.Validate());

    public IRelayCommand PhoneNumberIRelayCommand => new RelayCommand(() => this.PhoneNumber.Validate());

    public IRelayCommand EmailAddressIRelayCommand => new RelayCommand(() => this.EmailAddress.Validate());

    
    [ObservableProperty]
    private ImageSource image;
    
    private FileResult selectedFile;

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }

    private async Task OnSubmitAsync() => await asyncButtonAction();


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        bool hasValue = query.TryGetValue("Jury", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new judge";
            return;
        }

        JuryModel judge = result as JuryModel;

        this.Id = judge.Id;
        this.Name.Value = judge.Name.Value;
        this.PhoneNumber.Value = judge.PhoneNumber.Value;
        this.EmailAddress.Value = judge.EmailAddress.Value;

        if (!string.IsNullOrEmpty(judge.WebContentLink))
        {
            Image = new UriImageSource
            {
                Uri = new Uri(judge.WebContentLink),
                CacheValidity = new TimeSpan(10, 0, 0, 0)
            };
        }

        asyncButtonAction = OnUpdateAsync;
        Title = "Update judge";
    }


    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        await UploadImageAsync();

        var result = await judgeService.CreateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Judge saved.";

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

        await UploadImageAsync();


        var result = await judgeService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Judge saved.";
        var title = result.IsError ? "Error" : "Informtaion";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task OnImageSelectAsync()
    {
        selectedFile = await FilePicker.PickAsync(new PickOptions
        {
            FileTypes = FilePickerFileType.Images,
            PickerTitle = "please select the motorcycle image"
        });

        if (selectedFile is null)
        {
            return;
        }

        var stream = await selectedFile.OpenReadAsync();
        Image = ImageSource.FromStream(() => stream);
    }

    private async Task UploadImageAsync()
    {
        if (selectedFile is null)
        {
            return;
        }

        var imageUploadResult = await googleDriveService.UploadFileAsync(selectedFile);

        var message = imageUploadResult.IsError ? imageUploadResult.FirstError.Description : "Judge image uploaded";
        var title = imageUploadResult.IsError ? "Error" : "Information";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");

        this.ImageId = imageUploadResult.IsError ? null : imageUploadResult.Value.Id;
        this.WebContentLink = imageUploadResult.IsError ? null : imageUploadResult.Value.WebContentLink;

    }

    private void ClearForm()
    {
        this.Name.Value = null;
        this.PhoneNumber.Value = null;
        this.EmailAddress.Value = null;

        this.Image = null;
        this.selectedFile = null;
        this.ImageId = null;
        this.WebContentLink = null;
    }

    private bool IsFormValid()
    {
        this.Name.Validate();
        this.PhoneNumber.Validate();
        this.EmailAddress.Validate();

        return this.Name.IsValid &&
            this.PhoneNumber.IsValid &&
            this.EmailAddress.IsValid;
    }
}