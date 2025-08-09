
namespace UiTesterDemo.Services
{
    // The mock implementation. It implements both interfaces.
    public class MockWeatherService : IWeatherService, IMockManager
    {
        private double _mockTemperature = 15.0; // Default mock temperature

        // This method is from IWeatherService
        public Task<double> GetCurrentTemperatureAsync(double latitude, double longitude)
        {
            // It simply returns the current mock value.
            return Task.FromResult(_mockTemperature);
        }

        // This method is from IMockManager, used by the UI tester
        public void SetTemperature(double temp)
        {
            _mockTemperature = temp;
        }
    }
}
