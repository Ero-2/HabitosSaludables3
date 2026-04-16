using HabitosSaludables.ViewModels;

namespace HabitosSaludables.Views
{
    public partial class MisHabitosPage : ContentPage
    {
        public MisHabitosPage(MisHabitosViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }
    }
}