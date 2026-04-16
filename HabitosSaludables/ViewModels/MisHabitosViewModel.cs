using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitosSaludables.Models;
using HabitosSaludables.Services;
using System.Collections.ObjectModel;

namespace HabitosSaludables.ViewModels
{
    public partial class MisHabitosViewModel : ObservableObject
    {
        private readonly DatabaseService _databaseService;

        [ObservableProperty]
        private ObservableCollection<Habito> _habitos = new();

        [ObservableProperty]
        private bool _isRefreshing;

        public MisHabitosViewModel(DatabaseService databaseService)
        {
            _databaseService = databaseService;
            LoadHabitosCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadHabitos()
        {
            IsRefreshing = true;
            // Reiniciar estado diario antes de cargar
            await _databaseService.ResetDailyCompletions();
            var lista = await _databaseService.GetHabitosAsync();
            _habitos.Clear();
            foreach (var h in lista)
                _habitos.Add(h);
            IsRefreshing = false;
        }

        [RelayCommand]
        private async Task ToggleCompletado(Habito habito)
        {
            if (habito.CompletadoHoy)
            {
                await Shell.Current.DisplayAlert("Info", "Ya completaste este hábito hoy", "OK");
                return;
            }

            var exito = await _databaseService.ToggleCompletadoAsync(habito);
            if (exito)
            {
                // Actualizar la vista
                var index = Habitos.IndexOf(habito);
                if (index != -1) Habitos[index] = habito;
                await Shell.Current.DisplayAlert("¡Felicidades!",
                    $"Completaste {habito.Nombre}\nRacha actual: {habito.RachaActual} día(s)", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Error", "No se pudo marcar", "OK");
            }
        }

        [RelayCommand]
        private async Task EliminarHabito(Habito habito)
        {
            var confirm = await Shell.Current.DisplayAlert("Confirmar",
                $"¿Eliminar hábito '{habito.Nombre}'?", "Sí", "No");
            if (confirm)
            {
                await _databaseService.DeleteHabitoAsync(habito);
                Habitos.Remove(habito);
            }
        }

        [RelayCommand]
        private async Task AgregarNuevoHabito()
            => await Shell.Current.GoToAsync("AgregarHabitoPage");
    }
}