using HabitosSaludables.Converters;
using HabitosSaludables.Views;
using Microsoft.Extensions.DependencyInjection;

namespace HabitosSaludables
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          
            Routing.RegisterRoute("MisHabitosPage", typeof(MisHabitosPage));
            Routing.RegisterRoute("AgregarHabitoPage", typeof(AgregarHabitoPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}