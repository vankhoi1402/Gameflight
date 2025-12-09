using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static bool isLoading = false;

    public static void LoadScene(string sceneName)
    {
        if (isLoading) return;

        isLoading = true;
        SceneManager.LoadScene(sceneName);
        isLoading = false;
    }

    public static void QuitGame()
    {
        Debug.Log("Quit Game...");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
