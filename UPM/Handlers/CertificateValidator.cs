using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class CertificateValidator : ICertificateValidator
{
	private readonly HashSet<string> _knownSources = new HashSet<string>();

	public CertificateValidator(string resourcePath)
	{
		LoadKnownSourcesFromResources(resourcePath);
	}

	public CertificateValidator(IEnumerable<string> knownSources)
	{
		AddKnownSources(knownSources);
	}

	public async Task<CertificateValidator> InitializeFromRemote(string url)
	{
		await LoadKnownSourcesFromRemote(url);
		return this;
	}

	private void LoadKnownSourcesFromResources(string resourcePath)
	{
		TextAsset textAsset = Resources.Load<TextAsset>(resourcePath);

		if (textAsset != null)
		{
			var sources = textAsset.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
			AddKnownSources(sources);
		}
		else
		{
			Debug.LogError($"Known sources file not found in Resources: {resourcePath}");
		}
	}

	private async Task LoadKnownSourcesFromRemote(string url)
	{
		UnityWebRequest request = UnityWebRequest.Get(url);
		await request.SendWebRequest();

		if (request.result == UnityWebRequest.Result.Success)
		{
			var sources = request.downloadHandler.text.Split(new[] { '\r', '\n' }, System.StringSplitOptions.RemoveEmptyEntries);
			AddKnownSources(sources);
		}
		else
		{
			Debug.LogError($"Failed to load known sources from remote URL: {url}, Error: {request.error}");
		}
	}

	private void AddKnownSources(IEnumerable<string> knownSources)
	{
		foreach (var source in knownSources)
		{
			_knownSources.Add(source);
		}
	}

	public bool ValidateCertificate(string url)
	{
		return _knownSources.Contains(url);
	}

}