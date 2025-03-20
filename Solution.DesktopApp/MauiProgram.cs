using Solution.DesktopApp.Views;

namespace Solution.DesktopApp;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseMauiApp<App>()
               .UseMauiCommunityToolkit(options =>
               {
                   options.SetShouldEnableSnackbarOnWindows(true);
               })
               .UseMauiCommunityToolkitMarkup()
               .UseFontConfiguration()
               .UseAppConfigurations()
               .UseAppSettingsMapping()
               .UseDIConfiguration()
               .UseMsSqlServer();
        Routing.RegisterRoute(nameof(CompetitionListView), typeof(CompetitionListView));
        Routing.RegisterRoute(nameof(CreateOrEditCompetitionView), typeof(CreateOrEditCompetitionView));

        builder.Services.AddTransient<CreateOrEditCompetitionViewModel>();
        builder.Services.AddTransient<CreateOrEditCompetitionView>();

        builder.Services.AddTransient<CompetitionListViewModel>();
        builder.Services.AddTransient<CompetitionListView>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
