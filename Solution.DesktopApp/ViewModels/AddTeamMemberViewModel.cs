using System.Threading.Tasks;
using Solution.Core.Models;
using Solution.ValidationLibrary;
using System.Windows.Input;
using Microsoft.Maui.Storage;

namespace Solution.DesktopApp.ViewModels
{
    public class AddTeamMemberViewModel
    {
        public IAsyncRelayCommand AppearingCommand => new AsyncRelayCommand(OnAppearingAsync);
        public IAsyncRelayCommand DisappearingCommand => new AsyncRelayCommand(OnDisappearingAsync);
        public IAsyncRelayCommand SubmitCommand => new AsyncRelayCommand(OnSubmitAsync);
        public IAsyncRelayCommand ImageSelectCommand => new AsyncRelayCommand(OnImageSelectAsync);
        public ICommand ValidationCommand => new RelayCommand(OnValidate);

        private delegate Task ButtonActionDelagate();
        private ButtonActionDelagate asyncButtonAction;

        [ObservableProperty]
        private ImageSource Image;

        private FileResult selectedFile = null;

        public MemberModel Member { get; set; }

        public AddTeamMemberViewModel()
        {
            Member = new MemberModel();
        }

        private async Task OnAppearingAsync()
        {
            
        }

        private async Task OnDisappearingAsync()
        {
            
        }

        private async Task OnSubmitAsync() => await asyncButtonAction();

        private async Task OnImageSelectAsync()
        {
            var selectedFile = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Please select the motorcycle image"
            });

            if (selectedFile is null)
            {
                return;
            }

            var stream = await selectedFile.OpenReadAsync();
            Image = ImageSource.FromStream(() => stream);
        }

        private void OnValidate()
        {
            Member.Name.Validate();
        }
    }
}
