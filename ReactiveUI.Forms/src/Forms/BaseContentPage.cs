using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Text;
using ReactiveUI.XamForms;

namespace ReactiveUI.Forms
{
    public class BaseContentPage<TViewModel> : ReactiveContentPage<TViewModel>
        where TViewModel : class
    {
        protected CompositeDisposable SubscriptionDisposables { get; } = new CompositeDisposable();
    }
}