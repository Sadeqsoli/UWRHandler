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
