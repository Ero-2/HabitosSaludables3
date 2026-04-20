using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitosSaludables.Models;
using System.Collections.ObjectModel;

namespace HabitosSaludables.ViewModels
{
    public partial class PerfilViewModel : ObservableObject
    {
        // ── Datos del usuario ──────────────────────────────────────────────
        [ObservableProperty]
        private string nombreUsuario = "Carlos García";

        [ObservableProperty]
        private string email = "carlos@correo.com";

        [ObservableProperty]
        private string iniciales = "CG";

        [ObservableProperty]
        private string nivelUsuario = "🌱 Principiante";

        // ── Estadísticas ───────────────────────────────────────────────────
        [ObservableProperty]
        private int totalHabitos = 0;

        [ObservableProperty]
        private int rachaMaxima = 0;

        [ObservableProperty]
        private int diasActivo = 0;

        [ObservableProperty]
        private int metaDiaria = 3;

        // ── Configuración ──────────────────────────────────────────────────
        [ObservableProperty]
        private bool notificacionesActivas = true;

        [ObservableProperty]
        private bool isLoading;

        // ── Logros ─────────────────────────────────────────────────────────
        public ObservableCollection<Logro> Logros { get; } = new();

        // ──────────────────────────────────────────────────────────────────
        public PerfilViewModel()
        {
            CargarDatosMock();
        }

        // ── Datos de prueba (reemplazar con servicios reales después) ──────
        private void CargarDatosMock()
        {
            TotalHabitos = 4;
            RachaMaxima = 7;
            DiasActivo = 12;
            ActualizarNivel();
            CargarLogros();
        }

        [RelayCommand]
        public async Task CargarPerfilAsync()
        {
            IsLoading = true;
            try
            {
                await Task.Delay(300);
                CargarDatosMock();
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ActualizarNivel()
        {
            NivelUsuario = RachaMaxima switch
            {
                >= 90 => "🏆 Leyenda",
                >= 30 => "💎 Experto",
                >= 14 => "🔥 Constante",
                >= 7 => "⭐ Comprometido",
                _ => "🌱 Principiante"
            };
        }

        private void CargarLogros()
        {
            Logros.Clear();
            Logros.Add(new Logro { Titulo = "Primer día", Emoji = "🌱", Descripcion = "Completaste tu primer hábito", Desbloqueado = DiasActivo >= 1 });
            Logros.Add(new Logro { Titulo = "7 seguidos", Emoji = "🔥", Descripcion = "Racha de 7 días", Desbloqueado = RachaMaxima >= 7 });
            Logros.Add(new Logro { Titulo = "Mes perfecto", Emoji = "💎", Descripcion = "Racha de 30 días", Desbloqueado = RachaMaxima >= 30 });
            Logros.Add(new Logro { Titulo = "Coleccionista", Emoji = "📚", Descripcion = "5 hábitos activos", Desbloqueado = TotalHabitos >= 5 });
            Logros.Add(new Logro { Titulo = "Leyenda", Emoji = "🏆", Descripcion = "Racha de 90 días", Desbloqueado = RachaMaxima >= 90 });
        }

        [RelayCommand]
        private async Task EditarPerfilAsync()
        {
            await Shell.Current.GoToAsync("editarPerfil");
        }

        [RelayCommand]
        private async Task EditarMetaAsync()
        {
            string? result = await Shell.Current.CurrentPage.DisplayPromptAsync(
                title: "Meta diaria",
                message: "¿Cuántos hábitos quieres completar por día?",
                accept: "Guardar",
                cancel: "Cancelar",
                placeholder: MetaDiaria.ToString(),
                maxLength: 2,
                keyboard: Keyboard.Numeric,
                initialValue: MetaDiaria.ToString());

            if (int.TryParse(result, out int meta) && meta > 0 && meta <= 20)
            {
                MetaDiaria = meta;
                Preferences.Set("meta_diaria", meta);
            }
        }

        [RelayCommand]
        private async Task EditarTemaAsync()
        {
            await Shell.Current.DisplayAlert("Tema", "Próximamente disponible.", "OK");
        }

        [RelayCommand]
        private async Task ExportarDatosAsync()
        {
            bool confirmar = await Shell.Current.CurrentPage.DisplayAlert(
                "Exportar datos",
                "Se generará un archivo con tu historial de hábitos.",
                "Exportar", "Cancelar");

            if (confirmar)
                await Shell.Current.DisplayAlert("Exportar", "Función próximamente disponible.", "OK");
        }

        [RelayCommand]
        private async Task CerrarSesionAsync()
        {
            bool confirmar = await Shell.Current.CurrentPage.DisplayAlert(
                "Cerrar sesión",
                "¿Estás seguro que deseas cerrar sesión?",
                "Cerrar sesión", "Cancelar");

            if (confirmar)
            {
                Preferences.Remove("usuario_token");
                await Shell.Current.GoToAsync("//bienvenida");
            }
        }

        public static string ObtenerIniciales(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre)) return "U";
            var partes = nombre.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return partes.Length >= 2
                ? $"{partes[0][0]}{partes[1][0]}".ToUpper()
                : partes[0][0].ToString().ToUpper();
        }
    }
}