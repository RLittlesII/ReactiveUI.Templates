using System;
using System.Collections.Generic;
using System.Text;
using Splat;

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

        public virtual void NavigateToStart()
        {
            Router.NavigateAndReset.Execute(new MainViewModel()).Subscribe();
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
        }

        protected virtual void RegisterServices()
        {
        }

        protected virtual void RegisterViewModels()
        {
        }

        private void InitializeReactiveUI()
        {
            Locator.CurrentMutable.InitializeSplat();
            Locator.CurrentMutable.InitializeReactiveUI();
        }
    }
}