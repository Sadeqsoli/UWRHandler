using UnityEditor;
using UnityEngine;

public class KnownSourcesGenerator : EditorWindow
{
	private string fileName = "KnownSources";
	private KnownSources knownSources;

	[MenuItem("SleeplessDev/Known Sources Generator")]
	public static void ShowWindow()
	{
		GetWindow<KnownSourcesGenerator>("Known Url Source Generator");
	}

	private void OnGUI()
	{
		GUILayout.Label("Generate Known Url Sources", EditorStyles.boldLabel);

		fileName = EditorGUILayout.TextField("File Name", fileName);

		if (GUILayout.Button("Create Known Url Sources File"))
		{
			CreateKnownSourcesFile();
		}

		if (knownSources != null)
		{
			Editor knownSourcesEditor = Editor.CreateEditor(knownSources);
			knownSourcesEditor.OnInspectorGUI();
		}
	}

	private void CreateKnownSourcesFile()
	{
		knownSources = ScriptableObject.CreateInstance<KnownSources>();

		string path = $"Assets/Resources/{fileName}.asset";
		AssetDatabase.CreateAsset(knownSources, path);
		AssetDatabase.SaveAssets();

		EditorUtility.FocusProjectWindow();
		Selection.activeObject = knownSources;

		Debug.Log("Known Sources file created at: " + path);
	}
}
