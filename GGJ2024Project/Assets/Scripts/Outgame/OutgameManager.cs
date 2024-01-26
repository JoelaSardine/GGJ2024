using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutgameManager : MonoBehaviour
{
    public void Button_LaunchScene(string sceneName)
    {
	    UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void Button_Quit()
    {
#if UNITY_EDITOR
	    UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
