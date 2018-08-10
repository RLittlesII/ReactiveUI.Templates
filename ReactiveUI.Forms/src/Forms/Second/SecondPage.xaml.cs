using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReactiveUI.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecondPage : BaseContentPage<SecondViewModel>
    {
        public SecondPage()
        {
            InitializeComponent();
        }
    }
}