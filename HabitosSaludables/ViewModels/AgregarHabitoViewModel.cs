using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitosSaludables.Models;
using HabitosSaludables.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace HabitosSaludables.ViewModels
{
    public partial class AgregarHabitoViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private string _nombre = string.Empty;

        [ObservableProperty]
        private string _categoria = "Salud";

        [ObservableProperty]
        private string _icono = "🌿";

        public List<string> Categorias { get; } = new() { "Salud", "Deporte", "Alimentación", "Mental", "Otros" };

        public AgregarHabitoViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [RelayCommand]
        private async Task GuardarHabito()
        {
            if (string.IsNullOrWhiteSpace(Nombre))
            {
                await Shell.Current.DisplayAlert("Error", "El nombre es obligatorio", "OK");
                return;
            }

            var nuevo = new Habito
            {
                Nombre = Nombre,
                Categoria = Categoria,
                Icono = Icono,
                FechaCreacion = DateTime.Now
            };

            await _databaseService.SaveHabitoAsync(nuevo);
            await Shell.Current.DisplayAlert("Éxito", "Hábito agregado", "OK");
            await Shell.Current.GoToAsync(".."); // Regresa a la página anterior
        }

        [RelayCommand]
        private async Task Cancelar() => await Shell.Current.GoToAsync("..");
    }
}