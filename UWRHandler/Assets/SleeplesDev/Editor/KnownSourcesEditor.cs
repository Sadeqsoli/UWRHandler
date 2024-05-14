using SleeplessDev;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(KnownSources))]
public class KnownSourcesEditor : Editor
{
	public override void OnInspectorGUI()
	{
		var knownSources = (KnownSources)target;

		knownSources.sourceMode = (SourceMode)EditorGUILayout.EnumPopup("Source Mode", knownSources.sourceMode);

		if (knownSources.sourceMode == SourceMode.Remote)
		{
			knownSources.targetUrl = EditorGUILayout.TextField("Remote URL", knownSources.targetUrl);
		}
		else
		{
			EditorGUILayout.LabelField("Local Sources");
			SerializedProperty sourcesProperty = serializedObject.FindProperty("sources");
			EditorGUILayout.PropertyField(sourcesProperty, true);
		}

		serializedObject.ApplyModifiedProperties();

		if (GUILayout.Button("Save Known Sources"))
		{
			SaveKnownSources(knownSources);
		}
	}

	private void SaveKnownSources(KnownSources knownSources)
	{
		string path = AssetDatabase.GetAssetPath(knownSources);
		EditorUtility.SetDirty(knownSources);
		AssetDatabase.SaveAssets();
		Debug.Log("Known Sources saved at: " + path);
	}
}
