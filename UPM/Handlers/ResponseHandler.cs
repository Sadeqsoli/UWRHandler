using UnityEngine;
using UnityEngine.Networking;

namespace SleeplessDev
{
	public class ResponseHandler : IResponseHandler
	{
		public void HandleFailedRequest(UnityWebRequest response)
		{
			if (!string.IsNullOrEmpty(response.error))
			{
				Debug.Log(response.result + ": " + response.error);
			}
		}

		public void HandleFailedRequest(HttpResponse response)
		{
			if (response.IsTimeout)
			{
				Debug.Log("Message: The request was timed out.");
			}
			if (response.ResponseCode == 401)
			{
				//TODO: Handle token refresh logic
			}

			if (!string.IsNullOrEmpty(response.Error))
			{
				Debug.Log(response.Result + ": " + response.Error);
			}
		}
	}
}
