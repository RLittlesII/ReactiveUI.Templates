using ReactiveUI;
using Splat;
using Template.Detail;
using Template.Welcome;

namespace Template
{
    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public RoutingState Router { get; private set; }

        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState testRouter = null)
        {
            Router = testRouter ?? new RoutingState();
            dependencyResolver ??= Locator.CurrentMutable;

            // Bind 
            RegisterParts(dependencyResolver);

            // TODO: This is a good place to set up any other app startup tasks
            
            // Navigate to the opening page of the application
            Router.Navigate.Execute(new WelcomeViewModel(this));
        }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));

            dependencyResolver.Register(() => new WelcomeView(), typeof(IViewFor<WelcomeViewModel>));
            dependencyResolver.Register(() => new DetailView(), typeof(IViewFor<DetailViewModel>));
        }
    }
}