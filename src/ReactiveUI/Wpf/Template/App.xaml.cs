using System.Reactive;
using System.Windows;

namespace Template
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            MessageInteractions.ShowMessage.RegisterHandler(context =>
            {
                MessageBox.Show(context.Input);
                context.SetOutput(Unit.Default);
            });
        }
    }
}