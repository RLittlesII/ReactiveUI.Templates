using System.Reactive;
using ReactiveUI;

namespace Template.Welcome
{
    public interface IWelcomeViewModel : IRoutableViewModel
    {
        ReactiveCommand<Unit, Unit> HelloWorld { get; }
        ReactiveCommand<Unit, Unit> NavigateToSecond { get; }
    }
}