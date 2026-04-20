using HabitosSaludables.ViewModels;

namespace HabitosSaludables.Views
{
    public partial class PerfilPage : ContentPage   // 👈 partial + ContentPage
    {
        public PerfilPage()
        {
            InitializeComponent();
            BindingContext = new PerfilViewModel();
        }
    }
}