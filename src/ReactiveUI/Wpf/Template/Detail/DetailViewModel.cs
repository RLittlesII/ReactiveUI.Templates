using System.Reactive;
using ReactiveUI;

namespace Template.Detail
{
    public class DetailViewModel : ReactiveObject, IRoutableViewModel
    {
        public DetailViewModel(IScreen hostScreen)
        {
            HostScreen = hostScreen;

            this.Back = HostScreen.Router.NavigateBack;
        }

        public string UrlPathSegment => "Detail";
        public IScreen HostScreen { get; }

        public ReactiveCommand<Unit, Unit> Back { get; }
    }
}