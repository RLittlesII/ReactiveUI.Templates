using ReactiveUI;
using Splat;

namespace Template
{
    public class MainViewModel : ReactiveObject, IRoutableViewModel
    {
        public MainViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }

        public string UrlPathSegment { get; }
        public IScreen HostScreen { get; }
    }
}