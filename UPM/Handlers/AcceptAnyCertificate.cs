using UnityEngine.Networking;

public class AcceptAnyCertificate : CertificateHandler
{
	protected override bool ValidateCertificate(byte[] certificateData) => true;
}