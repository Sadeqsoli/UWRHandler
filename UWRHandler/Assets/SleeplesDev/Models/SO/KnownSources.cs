using SleeplessDev;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KnownUrlSources", menuName = "SO/KnownSources", order = 1)]
public class KnownSources : ScriptableObject
{
	public SourceMode sourceMode;
	public string targetUrl;
	public List<string> sources;
}