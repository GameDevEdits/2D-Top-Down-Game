using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    public void OnButtonClick()
    {
        // Assuming "MainMenu" is the name of your main menu scene.
        SceneManager.LoadScene("Main Menu");
    }
}
