using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcherWindow : OdinEditorWindow
{
    [MenuItem("Window/Scene Switcher")]
    private static void OpenWindow()
    {
        var window = GetWindow<SceneSwitcherWindow>();
        window.titleContent.text = "Scene Switcher";
        window.Show();
    }

    [Button]
    public void Preload()
    {
        EditorSceneManager.SaveModifiedScenesIfUserWantsTo(SceneUtils.GetAllOpenedScenes());
        var scene = EditorBuildSettings.scenes[0];
        EditorSceneManager.OpenScene(scene.path);
    }
    
    [Button]
    public void MainMenu()
    {
        EditorSceneManager.SaveModifiedScenesIfUserWantsTo(SceneUtils.GetAllOpenedScenes());
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(ScenesSettings.Instance.mainMenu.editorAsset));
    }
    
    [Button]
    public void MainGame()
    {
        EditorSceneManager.SaveModifiedScenesIfUserWantsTo(SceneUtils.GetAllOpenedScenes());
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(ScenesSettings.Instance.mainGame.editorAsset));
    }
    /*
    [Button]
    public void LoadLevel(int index)
    {
        if(index < 0 || index >= ScenesSettings.Instance.levels.Length) return;
            
        EditorSceneManager.SaveModifiedScenesIfUserWantsTo(SceneUtils.GetAllOpenedScenes());
        EditorSceneManager.OpenScene(AssetDatabase.GetAssetPath(ScenesSettings.GetLevelReference(index).editorAsset));
    }
    */
    
}
