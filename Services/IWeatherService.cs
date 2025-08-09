
namespace UiTesterDemo.Services
{
    // The abstraction for our service. The ViewModel will only know about this interface.
    public interface IWeatherService
    {
        Task<double> GetCurrentTemperatureAsync(double latitude, double longitude);
    }
}
