using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI.XamForms;
using Splat;
using Xamarin.Forms;

namespace ReactiveUI.Forms
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; protected set; }

        public void Initialize()
        {
            InitializeReactiveUI();
            RegisterScreen();
            RegisterPages();
            RegisterServices();
            RegisterPlatformServices();
            RegisterViewModels();
        }

        public virtual Page NavigateToStart()
        {
            Router.NavigateAndReset.Execute(new MainViewModel()).Subscribe();
            return new RoutedViewHost();
        }

        public virtual void RegisterPlatformServices()
        {
        }

        public virtual void RegisterScreen()
        {
            Router = new RoutingState();
            Locator.CurrentMutable.RegisterConstant(this, typeof(IScreen));
        }

        protected virtual void RegisterPages()
        {
            Locator.CurrentMutable.Register(() => new MainPage(), typeof(IViewFor<MainViewModel>));
            Locator.CurrentMutable.Register(() => new SecondPage(), typeof(IViewFor<SecondViewModel>));
        }

        protected virtual void RegisterServices()
        {
        }

        protected virtual void RegisterViewModels()
        {
            Locator.CurrentMutable.Register<MainViewModel>(() => new MainViewModel());
            Locator.CurrentMutable.Register<SecondViewModel>(() => new SecondViewModel());
        }

        private void InitializeReactiveUI()
        {
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();
        }
    }
}