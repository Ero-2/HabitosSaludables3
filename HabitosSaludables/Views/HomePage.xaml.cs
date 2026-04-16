using HabitosSaludables.ViewModels;

namespace HabitosSaludables.Views;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void OnRegistrarActividadClicked(object sender, EventArgs e)
    {
        _viewModel.RegistrarActividadCommand.Execute(null);
    }

    private void OnVerEstadisticasClicked(object sender, EventArgs e)
    {
        _viewModel.VerEstadisticasCommand.Execute(null);
    }

    private void OnConfiguracionClicked(object sender, EventArgs e)
    {
        _viewModel.ConfiguracionCommand.Execute(null);
    }

    private void OnLogoutClicked(object sender, EventArgs e)
    {
        _viewModel.CerrarSesionCommand.Execute(null);
    }
}