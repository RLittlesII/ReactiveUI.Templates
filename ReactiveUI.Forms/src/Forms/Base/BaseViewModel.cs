using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using Splat;

namespace ReactiveUI.Forms
{
    public class BaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public IScreen HostScreen { get; }

        public string UrlPathSegment { get; }

        protected CompositeDisposable SubscriptionDisposables { get; } = new CompositeDisposable();

        public BaseViewModel(IScreen screen = null)
        {
            HostScreen = screen ?? Locator.Current.GetService<IScreen>();
        }
    }
}