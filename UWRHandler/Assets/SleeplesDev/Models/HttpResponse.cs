using UnityEngine.Networking;

public class HttpResponse
{
	public long ResponseCode { get; set; }
	public string Error { get; set; }
	public bool IsTimeout { get; set; }
	public UnityWebRequest.Result Result { get; set; }
	public string Text { get; set; }
	public byte[] Data { get; set; }
}