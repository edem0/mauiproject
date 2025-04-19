using System.Threading.Tasks;
using Solution.Core.Models;
using Solution.ValidationLibrary;
using System.Windows.Input;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Solution.DesktopApp.ViewModels
{
    public partial class AddJudgeViewModel : ObservableObject
    {
        public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
        public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
        public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
        public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
        public ICommand ValidationCommand => new RelayCommand(OnValidate);

        private delegate Task ButtonActionDelegate();
        private ButtonActionDelegate asyncButtonAction;

        [ObservableProperty]
        private ImageSource image;

        [ObservableProperty]
        private bool isNameInvalid;

        [ObservableProperty]
        private bool isImageInvalid;

        private FileResult selectedFile;

        public JuryModel Judge { get; }

        public AddJudgeViewModel()
        {
            Judge = new JuryModel();
            asyncButtonAction = DefaultSubmitActionAsync;
        }

        private Task OnAppearingAsync()
        {
            return Task.CompletedTask;
        }

        private Task OnDisappearingAsync()
        {
            return Task.CompletedTask;
        }

        private async Task OnSubmitAsync()
        {
            OnValidate();

            if (!isNameInvalid && !isImageInvalid)
            {
                await asyncButtonAction();
            }
        }

        private Task DefaultSubmitActionAsync()
        {
            return Task.CompletedTask;
        }

        private async Task OnImageSelectAsync()
        {
#pragma warning disable CS8601
            selectedFile = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Please select an image"
            });
#pragma warning restore CS8601

            if (selectedFile != null)
            {
                var stream = await selectedFile.OpenReadAsync();
                Image = ImageSource.FromStream(() => stream);
            }
        }

        private void OnValidate()
        {
            var nameValue = Judge.Name?.Value?.Trim();
            IsNameInvalid = string.IsNullOrWhiteSpace(nameValue) || nameValue.Length < 3;
            IsImageInvalid = Image == null;
        }
    }
}


