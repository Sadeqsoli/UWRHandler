# UWRH - Unity Web Request Handler
UWRHnadler is a Unity package that handles HTTP requests following SOLID principles, providing a clean, maintainable, and extensible way to manage GET and POST requests with support for custom headers and certificates.

[![openupm](https://img.shields.io/npm/v/com.uwrhandler.uwr?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.uwrhandler.uwr/)
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
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SleeplessDev
{
	public class RequestManagerInitializer : MonoBehaviour
	{
		public KnownSources knownSources;

		private IRequestHandler _requestHandler;

		public event Action<string> OnResponseReceived;

		async void Start()
		{
			ICertificateValidator certificateValidator;

			if (knownSources.sourceMode == SourceMode.Remote)
			{
				var tempValidator = new CertificateValidator(new List<string>()); // Initialize with an empty list
				certificateValidator = await tempValidator.InitializeFromRemote(knownSources.targetUrl);
			}
			else
			{
				certificateValidator = new CertificateValidator(knownSources.sources);
			}

			ICertificateHandler certificateHandler = new CertificateHandler(certificateValidator);
			IResponseHandler responseHandler = new ResponseHandler();
			ISerializer serializer = new JsonSerializer();

			_requestHandler = new RequestManager(certificateHandler, responseHandler, serializer);
		}

		public async void FetchData()
		{
			//TODO: add your own url that works and also fill out known sources to accept all certificates https://api.example.com/data
			string url = "https://trustedsource.com/api/data";
			var response = await _requestHandler.SendGetRequestAsync(url, Header.None);

			if (string.IsNullOrEmpty(response.Error))
			{
				Debug.Log(response.Text);
				OnResponseReceived?.Invoke(response.Text);
			}
			else
			{
				Debug.LogError(response.Error);
				OnResponseReceived?.Invoke($"Error: {response.Error}");
			}
		}
	}
}

```

2. **Example Unity Web Request Handler**:

```csharp
using TMPro;
using UnityEngine;

namespace SleeplessDev.Examples
{
	public class ExampleUWRHandler : MonoBehaviour
	{
		public RequestManagerInitializer localRequestManager;
		public RequestManagerInitializer remoteRequestManager;
		public TextMeshProUGUI responseText;

		public void OnLocalButtonClicked()
		{
			localRequestManager.FetchData();
		}

		public void OnRemoteButtonClicked()
		{
			remoteRequestManager.FetchData();
		}

		void DisplayResponse(string response)
		{
			responseText.text = response;
		}

		private void Start()
		{
			// Attach event listeners
			localRequestManager.OnResponseReceived += DisplayResponse;
			remoteRequestManager.OnResponseReceived += DisplayResponse;
		}

		private void OnDestroy()
		{
			// Detach event listeners
			localRequestManager.OnResponseReceived -= DisplayResponse;
			remoteRequestManager.OnResponseReceived -= DisplayResponse;
		}
	}
}