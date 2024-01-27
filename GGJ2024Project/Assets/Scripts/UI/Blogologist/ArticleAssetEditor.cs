using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ArticlesAssets))]
public class ArticleAssetEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ArticlesAssets xmlReaderSettings = (ArticlesAssets)target;

        // Add a button to the inspector
        if (GUILayout.Button("Read Races XML"))
        {
            xmlReaderSettings.UpdateRacesArticles();
        }

        if (GUILayout.Button("Read Cultures XML"))
        {
            xmlReaderSettings.UpdateCultureArticles();
        }
    }
}
