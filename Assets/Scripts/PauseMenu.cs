using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public static bool isPaused;

    private static bool isReturningFromOtherScene = false;
    private static bool wasGamePausedBeforeLoadingIndex = false;

    public static bool WasGamePausedBeforeLoadingIndex
    {
        get { return wasGamePausedBeforeLoadingIndex; }
    }

    void Start()
    {
        if (!isReturningFromOtherScene)
        {
            if (!wasGamePausedBeforeLoadingIndex)
                pauseMenu.SetActive(false);
            else
            {
                pauseMenu.SetActive(true);
                isPaused = true;
            }
        }
        else
        {
            isReturningFromOtherScene = false;
            wasGamePausedBeforeLoadingIndex = false;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        AudioListener.pause = true; // Pause all audio
    }

    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        AudioListener.pause = false; // Resume all audio
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Resume all audio
        SceneManager.LoadScene("Main Menu");
    }

    public void GoToEnemyIndex()
    {
        if (isPaused)
            wasGamePausedBeforeLoadingIndex = true;

        Time.timeScale = 1f;
        AudioListener.pause = false; // Resume all audio
        SceneManager.LoadScene("EnemyIndex");

    }

    public void RetryRoom1()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false; // Resume all audio
        SceneManager.LoadScene("Room 1");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void SetReturningFromOtherScene(bool value)
    {
        isReturningFromOtherScene = value;
    }
}
