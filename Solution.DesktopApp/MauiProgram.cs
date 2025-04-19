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
        Routing.RegisterRoute(nameof(AddJuryView), typeof(AddJuryView));
        Routing.RegisterRoute(nameof(AddTeamView), typeof(AddTeamView));
        Routing.RegisterRoute(nameof(AddTeamMemberView), typeof(AddTeamMemberView));

        builder.Services.AddTransient<CreateOrEditCompetitionView>();
        builder.Services.AddTransient<CompetitionListView>();
        builder.Services.AddTransient<AddJuryView>();
        builder.Services.AddTransient<AddTeamView>();
        builder.Services.AddTransient<AddTeamMemberView>();

        builder.Services.AddTransient<CreateOrEditCompetitionViewModel>();
        builder.Services.AddTransient<CompetitionListViewModel>();
        builder.Services.AddTransient<AddJudgeViewModel>();
        builder.Services.AddTransient<AddTeamViewModel>();
        builder.Services.AddTransient<AddTeamMemberViewModel>();



#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
