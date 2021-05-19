using ReactiveUI;

namespace Template.Detail
{
    public partial class DetailView
    {
        public DetailView()
        {
            InitializeComponent();
            this.WhenActivated(disposable =>
            {
                disposable(this.BindCommand(ViewModel, vm => vm.Back, view => view.backButton));
            });
        }
    }
}