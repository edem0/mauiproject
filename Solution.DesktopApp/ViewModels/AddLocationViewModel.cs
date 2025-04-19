using Solution.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.DesktopApp.ViewModels;

[ObservableObject]
public partial class AddLocationViewModel(AppDbContext dbContext, ILocationService locationService) : LocationModel(), IQueryAttributable
{
    private delegate Task ButtonActionDelegate();
    private ButtonActionDelegate asyncButtonAction;

    [ObservableProperty]
    private string title;

    public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
    public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);

    public IRelayCommand AreaNameIRelayCommand => new RelayCommand(() => this.AreaName.Validate());

    public IRelayCommand HouseNumberIRelayCommand => new RelayCommand(() => this.HouseNumber.Validate());
    public IRelayCommand CityIndexChanged => new RelayCommand(() => this.City.Validate());

    public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);

    private async Task OnAppearingAsync() { }

    private async Task OnDisappearingAsync() { }

    [ObservableProperty]
    private ICollection<CityModel> cities;

    private async Task OnSubmitAsync() => await asyncButtonAction();

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        await Task.Run(() => LoadCities());

        bool hasValue = query.TryGetValue("Location", out object result);

        if (!hasValue)
        {
            asyncButtonAction = OnSaveAsync;
            Title = "Add new location";
            return;
        }

        LocationModel location = result as LocationModel;

        this.Id = location.Id;
        this.AreaName.Value = location.AreaName.Value;
        this.HouseNumber.Value = location.HouseNumber.Value;
        this.City.Value = location.City.Value;

        asyncButtonAction = OnUpdateAsync;
        Title = "Update location";
    }


    private async Task OnSaveAsync()
    {
        if (!IsFormValid())
        {
            return;
        }

        var result = await locationService.CreateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Location saved.";

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

        var result = await locationService.UpdateAsync(this);

        var message = result.IsError ? result.FirstError.Description : "Location saved.";
        var title = result.IsError ? "Error" : "Informtaion";

        await Application.Current.MainPage.DisplayAlert(title, message, "OK");
    }

    private async Task LoadCities()
    {
        Cities = await dbContext.Cities.AsNoTracking()
                                       .OrderBy(x => x.CityName)
                                       .Select(x => new CityModel(x))
                                       .ToListAsync();
    }

    private void ClearForm()
    {
        this.AreaName.Value = null;
        this.HouseNumber = null;
        this.City.Value = null;
    }

    private bool IsFormValid()
    {
        this.AreaName.Validate();
        this.HouseNumber.Validate();
        this.City.Validate();

        return (this.City?.IsValid ?? false) &&
            this.AreaName.IsValid &&
            this.HouseNumber.IsValid;
    }
}
