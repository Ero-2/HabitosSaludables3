using HabitosSaludables.ViewModels;

namespace HabitosSaludables.Views
{
    public partial class AgregarHabitoPage : ContentPage
    {
        public AgregarHabitoPage(AgregarHabitoViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}