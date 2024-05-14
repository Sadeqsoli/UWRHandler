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
			//TODO: add your own url that works and also fill out known sources to accept all certificates
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
