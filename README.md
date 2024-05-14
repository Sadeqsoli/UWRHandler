# UWRHnadler
UWRHnadler is a Unity package that handles HTTP requests following SOLID principles, providing a clean, maintainable, and extensible way to manage GET and POST requests with support for custom headers and certificates.
## Features

- **Single Responsibility Principle (SRP)**: Each class handles a specific responsibility.
- **Open/Closed Principle (OCP)**: Easily extend functionality without modifying existing code.
- **Liskov Substitution Principle (LSP)**: Derived classes are substitutable for their base types.
- **Interface Segregation Principle (ISP)**: Clients depend only on the interfaces they use.
- **Dependency Inversion Principle (DIP)**: Depend on abstractions rather than concrete implementations.

## Installation

1. Download the package from the [Releases](https://github.com/Sadeqsoli/UWRHnadler/releases) page.
2. In Unity, go to `Assets > Import Package > Custom Package`.
3. Select the downloaded package and import all files.
4. **(Optional)** For more complex JSON handling, install Newtonsoft Json.NET:
    - Open the Unity Package Manager (`Window > Package Manager`).
    - Click the `+` button and select `Add package from git URL...`.
    - Enter `https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git` and click `Add`.

## Contributing
Contributions are welcome! Please submit a pull request or open an issue to discuss your ideas.

## Usage

1. **Initialize the Request Manager**:

```csharp
using SleeplessDev;

public class Example : MonoBehaviour
{
    private IRequestHandler _requestHandler;

    private void Start()
    {
        ICertificateHandler certificateHandler = new CertificateHandler();
        IResponseHandler responseHandler = new ResponseHandler();
        ISerializer serializer = new JsonSerializer();
        _requestHandler = new RequestManager(certificateHandler, responseHandler, serializer);
    }
    public async void GetExample()
    {
        string url = "https://api.example.com/data";
        Header header = new Header(); // Customize as needed
        HttpResponse response = await _requestHandler.SendGetRequestAsync(url, header);
        
        if (!response.IsError)
        {
            Debug.Log(response.Text);
        }
    }
    public async void PostExample()
    {
        string url = "https://api.example.com/data";
        var data = new { Key = "value" }; // Your data object
        Header header = new Header(); // Customize as needed
        HttpResponse response = await _requestHandler.SendPostRequestAsync(url, data, header);
        
        if (!response.IsError)
        {
            Debug.Log(response.Text);
        }
    }
}
