using System;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Template.Detail;

namespace Template.Welcome
{
    public class WelcomeViewModel : ReactiveObject, IWelcomeViewModel
    {
        public WelcomeViewModel(IScreen screen)
        {
            HostScreen = screen;

            HelloWorld = ReactiveCommand.CreateFromObservable(() => MessageInteractions.ShowMessage.Handle("It works!!!"));
            NavigateToSecond = ReactiveCommand.CreateFromTask(async () => await HostScreen.Router.Navigate.Execute(new DetailViewModel(HostScreen)).Select(_ => Unit.Default));

            this.WhenNavigatedTo(Bar);
        }
        public string UrlPathSegment => "welcome";

        public IScreen HostScreen { get; protected set; }

        public ReactiveCommand<Unit, Unit> HelloWorld { get; protected set; }

        public ReactiveCommand<Unit, Unit> NavigateToSecond { get; }


        private IDisposable Bar()
        {
            return Disposable.Create(() => Foo());
        }

        private void Foo()
        {
            if (true) { }
        }
    }
}