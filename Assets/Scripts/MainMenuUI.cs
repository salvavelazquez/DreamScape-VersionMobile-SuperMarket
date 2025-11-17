using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";
    [SerializeField] private string calibrationSceneName = "Calibration";

    public void OnPlayClicked()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void OnCalibrateClicked()
    {
        SceneManager.LoadScene(calibrationSceneName);
    }

    public void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
