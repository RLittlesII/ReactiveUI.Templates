using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ReactiveUI.Forms
{
    public partial class MainPage : BaseContentPage<MainViewModel>
    {
        public MainPage()
        {
            InitializeComponent();

            this.WhenActivated(dispose =>
            {
                dispose(this.BindCommand(ViewModel, vm => vm.SecondCommand, page => page.SecondButton));
            });
        }
    }
}