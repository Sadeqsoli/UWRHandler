using SleeplessDev;
using System.Threading.Tasks;
using UnityEngine.Networking;

public interface IRequestHandler
{
	Task<HttpResponse> SendPostRequestAsync<T>(string url, T data, Header header);
	Task<HttpResponse> SendGetRequestAsync(string url, Header header);
}