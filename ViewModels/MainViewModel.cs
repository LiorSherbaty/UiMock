using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Options;
using System.Windows.Media;
using UiTesterDemo.Models;
using UiTesterDemo.Services;

namespace UiTesterDemo.ViewModels
{
    // The heart of our UI logic (the "VM" in MVVM).
    public partial class MainViewModel : ObservableObject
    {
        private readonly IWeatherService _weatherService;
        private readonly IMockManager? _mockManager;

        [ObservableProperty]
        private double _temperature;

        [ObservableProperty]
        private Brush _temperatureColor;

        [ObservableProperty]
        private bool _isMockMode;

        [ObservableProperty]
        private bool _isLoading;

        public MainViewModel(IWeatherService weatherService, IOptions<AppSettings> appSettings, IMockManager? mockManager = null)
        {
            _weatherService = weatherService;
            _mockManager = mockManager; // Will be null if not in mock mode
            _isMockMode = appSettings.Value.MockEnabled;
            _temperatureColor = Brushes.Black; // Default color

            // Load initial data
            LoadWeatherCommand.Execute(null);
        }

        [RelayCommand]
        private async Task LoadWeatherAsync()
        {
            IsLoading = true;
            // Fetching for a sample location (Düsseldorf, Germany)
            var temp = await _weatherService.GetCurrentTemperatureAsync(51.22, 6.77);
            UpdateTemperature(temp);
            IsLoading = false;
        }

        // --- These commands are for the UI Tester ---

        [RelayCommand(CanExecute = nameof(CanExecuteMockCommands))]
        private void SetHotTemp()
        {
            _mockManager?.SetTemperature(35);
            LoadWeatherCommand.Execute(null); // Re-run the logic to update the UI
        }

        [RelayCommand(CanExecute = nameof(CanExecuteMockCommands))]
        private void SetWarmTemp()
        {
            _mockManager?.SetTemperature(15);
            LoadWeatherCommand.Execute(null);
        }

        [RelayCommand(CanExecute = nameof(CanExecuteMockCommands))]
        private void SetColdTemp()
        {
            _mockManager?.SetTemperature(5);
            LoadWeatherCommand.Execute(null);
        }

        private bool CanExecuteMockCommands() => IsMockMode;


        // --- This is the UI logic we want to test ---
        private void UpdateTemperature(double temp)
        {
            Temperature = temp;

            if (temp > 30)
            {
                TemperatureColor = Brushes.Red;
            }
            else if (temp >= 10 && temp <= 30)
            {
                TemperatureColor = Brushes.Orange;
            }
            else
            {
                TemperatureColor = Brushes.Blue;
            }
        }
    }
}
