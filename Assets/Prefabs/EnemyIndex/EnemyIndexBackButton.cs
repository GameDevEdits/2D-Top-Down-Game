using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyIndexBackButton : MonoBehaviour
{
    public void GoBackToMainScene()
    {
        PauseMenu pauseMenu = FindObjectOfType<PauseMenu>(); // Find the PauseMenu instance
        if (pauseMenu != null)
        {
            if (PauseMenu.isPaused && !PauseMenu.WasGamePausedBeforeLoadingIndex)
            {
                // If the game was not previously paused before going to the Enemy Index, resume it
                pauseMenu.ResumeGame();
            }

            PauseMenu.SetReturningFromOtherScene(true); // Access SetReturningFromOtherScene using the class name
        }

        SceneManager.LoadScene("Room 1"); // Load the correct main scene
    }
}
