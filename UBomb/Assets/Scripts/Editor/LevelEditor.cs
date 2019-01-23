using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

/// <summary>
/// Level editor API GUI tools to assist in creating the level.
/// </summary>
[CustomEditor(typeof(LevelCreator))]
public class LevelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("GenerateBlocks"))
        {
            LevelCreator lc = (LevelCreator)target;
            lc.clear();
            lc.generateCleanBlocks();
            Debug.Log("Creating Level Grid Blocks");
        }

        if (GUILayout.Button("Clear"))
        {
            LevelCreator lc = (LevelCreator)target;
            lc.clear();
            Debug.Log("Clearing Level Blocks");
        }

        if (GUILayout.Button("Save"))
        {
            LevelCreator lc = (LevelCreator)target;
            lc.save();

            // after scriptable objects have been changed
            // tell the editor to flag these values as dirty so it is saved
            // when unity reloads
            EditorUtility.SetDirty(lc.levelToSaveTo);

            // we don't seem to need these to be able to reload successfully, setdirty is enough
            //EditorSceneManager.MarkSceneDirty(EditorSceneManager.GetActiveScene());
            //AssetDatabase.SaveAssets();

            Debug.Log("Saving Created Level");
        }

        if (GUILayout.Button("Load"))
        {
            LevelCreator lc = (LevelCreator)target;
            lc.load();
            Debug.Log("Loading Saved Created Level");
        }
    }
}
