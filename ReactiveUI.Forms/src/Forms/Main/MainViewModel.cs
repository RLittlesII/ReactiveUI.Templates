using System;
using System.Collections.Generic;
using System.Reactive;
using System.Text;

namespace ReactiveUI.Forms
{
    public class MainViewModel : BaseViewModel
    {
        public ReactiveCommand<Unit, Unit> SecondCommand { get; set; }

        public MainViewModel()
        {
            SecondCommand = ReactiveCommand.Create(ExecuteSecondCommand);
        }

        private void ExecuteSecondCommand()
        {
            HostScreen.Router.Navigate.Execute(new SecondViewModel()).Subscribe();
        }
    }
}