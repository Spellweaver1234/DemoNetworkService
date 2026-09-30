using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Demo.Services;

namespace Demo;

public class MainViewModel : INotifyPropertyChanged
{
    private readonly INetworkService _networkService;
    private bool _isOnline;

    public event PropertyChangedEventHandler? PropertyChanged;

    // Свойство для связи с разметкой. true — интернет есть, false — нет.
    public bool IsOnline
    {
        get => _isOnline;
        set
        {
            if (_isOnline != value)
            {
                _isOnline = value;
                OnPropertyChanged();
                // Автоматически обновляем инвертированное свойство для плашки
                OnPropertyChanged(nameof(IsOffline)); 
            }
        }
    }

    // Это свойство нужно для управления видимостью красной плашки
    public bool IsOffline => !IsOnline;

    public ICommand SendDataCommand { get; }

    public MainViewModel(INetworkService networkService)
    {
        _networkService = networkService;
        
        // Задаем начальное состояние
        IsOnline = _networkService.IsConnected;

        // Подписываемся на изменения сети
        _networkService.ConnectionChanged += OnNetworkChanged;

        // Команда для кнопки (активна, только когда есть интернет)
        SendDataCommand = new Command(
            execute: () => App.Current?.MainPage?.DisplayAlert("Успех", "Данные отправлены!", "OK"),
            canExecute: () => IsOnline
        );
    }

    private void OnNetworkChanged(object? sender, bool isConnected)
    {
        // Принудительно просим кнопку перепроверить свое состояние (активна/заблокирована)
        MainThread.BeginInvokeOnMainThread(() => 
        {
	        IsOnline = isConnected;
            ((Command)SendDataCommand).ChangeCanExecute();
        });
    }

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}