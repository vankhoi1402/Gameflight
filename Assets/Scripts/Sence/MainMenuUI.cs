using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "GamePlay";
    private void Start()
    {
        AudioManager.Instance.PlayMusic("music");
    }

    public void OnPlayButton()
    {
        SceneLoader.LoadScene(sceneToLoad);
    }

    public void OnQuitButton()
    {
        SceneLoader.QuitGame();
    }
}
