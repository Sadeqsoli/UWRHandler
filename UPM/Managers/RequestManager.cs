using System.Threading.Tasks;
using UnityEngine.Networking;
using UnityEngine;
using System;

namespace SleeplessDev
{
	public class RequestManager : MonoBehaviour, IRequestHandler
	{
		private readonly ICertificateHandler _certificateHandler;
		private readonly IResponseHandler _responseHandler;
		private readonly ISerializer _serializer;

		public RequestManager(ICertificateHandler certificateHandler, IResponseHandler responseHandler, ISerializer serializer)
		{
			_certificateHandler = certificateHandler;
			_responseHandler = responseHandler;
			_serializer = serializer;
		}

		public async Task<HttpResponse> SendGetRequestAsync(string url, Header header)
		{
			var uwr = UnityWebRequest.Get(url);
			uwr = SetRightHeaders(uwr, header);

			var response = await SendRequestAsync(uwr);
			return response;
		}

		public async Task<HttpResponse> SendPostRequestAsync<T>(string url, T data, Header header)
		{
			var uwr = new UnityWebRequest(url, UnityWebRequest.kHttpVerbPOST)
			{
				uploadHandler = new UploadHandlerRaw(_serializer.Serialize(data))
			};
			uwr = SetRightHeaders(uwr, header);

			var response = await SendRequestAsync(uwr);
			return response;
		}

		private async Task<HttpResponse> SendRequestAsync(UnityWebRequest uwr)
		{
			await uwr.SendWebRequest();

			if (uwr.result != UnityWebRequest.Result.Success)
			{
				_responseHandler.HandleFailedRequest(uwr);
				return new HttpResponse { Result = uwr.result, Error = uwr.error, ResponseCode = uwr.responseCode };
			}
			else
			{
				return new HttpResponse { Result = uwr.result, Text = uwr.downloadHandler.text };
			}
		}

		UnityWebRequest SetRightHeaders(UnityWebRequest uwr, Header header)
		{
			if (header == Header.None)
			{
				// No headers to set
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.B)
			{
				uwr.SetRequestHeader(DB.BHeader.Name, DB.BHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.BC)
			{
				uwr.SetRequestHeader(DB.BHeader.Name, DB.BHeader.Value);
				uwr.SetRequestHeader(DB.CHeader.Name, DB.CHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.A)
			{
				uwr.SetRequestHeader(DB.AHeader.Name, DB.AHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.C)
			{
				uwr.SetRequestHeader(DB.CHeader.Name, DB.CHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.AB)
			{
				uwr.SetRequestHeader(DB.AHeader.Name, DB.AHeader.Value);
				uwr.SetRequestHeader(DB.BHeader.Name, DB.BHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.AC)
			{
				uwr.SetRequestHeader(DB.AHeader.Name, DB.AHeader.Value);
				uwr.SetRequestHeader(DB.CHeader.Name, DB.CHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else if (header == Header.ABC)
			{
				uwr.SetRequestHeader(DB.AHeader.Name, DB.AHeader.Value);
				uwr.SetRequestHeader(DB.BHeader.Name, DB.BHeader.Value);
				uwr.SetRequestHeader(DB.CHeader.Name, DB.CHeader.Value);
				return _certificateHandler.AddCertificate(uwr);
			}
			else
			{
				throw new ArgumentOutOfRangeException(nameof(header));
			}
		}
	}
}
