using HabitosSaludables.Views;

namespace HabitosSaludables;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Registrar rutas adicionales (solo las que no están en XAML o las que necesitas)
        Routing.RegisterRoute(nameof(BienvenidaPage), typeof(BienvenidaPage));
        Routing.RegisterRoute(nameof(RegistroPage), typeof(RegistroPage));
        Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(PerfilPage), typeof(PerfilPage));
        Routing.RegisterRoute(nameof(MisHabitosPage), typeof(MisHabitosPage)); // 👈 Ruta para "mis-habitos"
    }
}