using UnityEngine.Networking;

public interface IResponseHandler
{
	void HandleFailedRequest(UnityWebRequest response);
	void HandleFailedRequest(HttpResponse response);
}