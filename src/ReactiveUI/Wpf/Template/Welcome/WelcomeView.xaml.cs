using ReactiveUI;

namespace Template.Welcome
{
    public partial class WelcomeView
    {
        public WelcomeView()
        {
            InitializeComponent();
            
            this.WhenActivated(disposable =>
            {
                disposable(this.WhenAnyValue(x => x.ViewModel).BindTo(this, x => x.DataContext));
                disposable(this.BindCommand(ViewModel, vm => vm.HelloWorld, view => view.helloWorldButton));
                disposable(this.BindCommand(ViewModel, vm => vm.NavigateToSecond, view => view.navigateButton));
            });
        }
    }
}