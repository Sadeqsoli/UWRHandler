using UnityEngine.Networking;

namespace SleeplessDev
{
	public class CertificateHandler : ICertificateHandler
	{
		private readonly ICertificateValidator _certificateValidator;

		public CertificateHandler(ICertificateValidator certificateValidator)
		{
			_certificateValidator = certificateValidator;
		}

		public UnityWebRequest AddCertificate(UnityWebRequest uwr)
		{
			// Assuming _certificateValidator validates known certificates properly
			if (_certificateValidator.ValidateCertificate(uwr.url))
			{
				uwr.certificateHandler = new AcceptAnyCertificate();
			}
			else
			{
				uwr.certificateHandler = new DefaultCertificateHandler();
			}

			return uwr;
		}
	}
}