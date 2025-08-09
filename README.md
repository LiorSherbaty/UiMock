# WPF UI Mocking & Testing Demo

This project is a demonstration of how to build a modern, testable WPF application using .NET. It showcases a clean architecture that separates UI logic from services, allowing for easy testing of the ViewModel by mocking external dependencies like web APIs.

This approach is crucial for developing robust applications, as it allows developers to test UI state logic (e.g., "does the text turn red when the temperature is hot?") instantly and reliably, without relying on network connectivity or live API endpoints.

---

## 🚀 Core Concepts Illustrated

* **MVVM (Model-View-ViewModel):** The application strictly follows the MVVM pattern to ensure a clean separation of concerns between the UI (View) and the application logic (ViewModel).
* **Dependency Injection (DI):** Services are injected into ViewModels using the `Microsoft.Extensions.Hosting` generic host builder. This decouples the ViewModel from concrete service implementations.
* **Configuration:** Application settings, such as API keys or feature flags, are managed through an `appsettings.json` file.
* **Mocking via Interfaces:** The ViewModel depends on an `IWeatherService` interface, not a concrete class. The DI container provides either a real API-calling service or a simple mock service based on a configuration flag.
* **SOLID Principles:** The architecture adheres to SOLID principles, particularly the Single Responsibility Principle and the Dependency Inversion Principle.

---

## 🛠️ Prerequisites

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (or newer)
* [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) with the ".NET desktop development" workload installed.

---

## ⚙️ Setup & Configuration

1.  **Clone the repository:**
    ```bash
    git clone <your-repository-url>
    cd <repository-folder>
    ```

2.  **Open in Visual Studio:**
    Open the `.sln` file in Visual Studio. The necessary NuGet packages should be restored automatically. If not, right-click the solution in the Solution Explorer and select "Restore NuGet Packages".

3.  **Configure the Application:**
    The application's behavior is controlled by the `appsettings.json` file.

    ```json
    {
      "AppSettings": {
        "MockEnabled": true,
        "ApiBaseUrl": "[https://api.open-meteo.com](https://api.open-meteo.com)"
      }
    }
    ```
    * `MockEnabled`:
        * `true`: The application will use the `MockWeatherService`. The UI Tester panel will be visible, allowing you to manually set the temperature to test the UI logic.
        * `false`: The application will use the `WeatherApiService` to call the live Open-Meteo weather API. The UI Tester panel will be hidden.
    * `ApiBaseUrl`: The base URL for the real weather API.

    > **Important:** In Visual Studio, ensure the `appsettings.json` file has its **"Copy to Output Directory"** property set to **"Copy if newer"**.

---

## ▶️ How to Run

Simply press **F5** or click the "Start" button in Visual Studio to build and run the application.

### Testing in Mock Mode

1.  Set `"MockEnabled": true` in `appsettings.json`.
2.  Run the application.
3.  The "UI TESTER" panel will be visible at the bottom of the window.
4.  Click the "Set Hot", "Set Warm", or "Set Cold" buttons to instantly change the temperature value and observe the corresponding color change in the UI.

### Running in Live Mode

1.  Set `"MockEnabled": false` in `appsettings.json`.
2.  Run the application.
3.  The application will make a live API call to fetch the current temperature for a sample location and display it with the appropriate color. The "UI TESTER" panel will not be visible.

---

## 📂 Project Structure


```
├── Convertes/
    ├── NotConverter.cs # It's used to invert the IsLoading boolean for the button's IsEnabled property
├── Models/
│   ├── AppSettings.cs      # C# class mapping to appsettings.json
│   └── WeatherForecast.cs  # DTO for the weather API response
├── Services/
│   ├── IWeatherService.cs  # Interface for the weather service (the contract)
│   ├── IMockManager.cs     # Special interface to control the mock service
│   ├── WeatherApiService.cs# Real implementation that calls the API
│   └── MockWeatherService.cs # Mock implementation for testing
├── ViewModels/
│   └── MainViewModel.cs    # Contains all UI logic and state
├── Views/
│   └── MainWindow.xaml     # The main application window
├── App.xaml.cs             # Application entry point, DI container setup
└── appsettings.json        # Configuration file
```
---

## 💻 Key Technologies

* WPF (.NET 8)
* C#
* MVVM Pattern
* Microsoft.Extensions.Hosting (for Dependency Injection & Configuration)
* CommunityToolkit.Mvvm (for modern MVVM implementation with source generators)
