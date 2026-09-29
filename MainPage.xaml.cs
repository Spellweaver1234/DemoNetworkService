namespace Demo;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        // Привязываем данные страницы к нашей ViewModel
        BindingContext = viewModel;
    }
}
