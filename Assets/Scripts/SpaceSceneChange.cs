using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceSceneChange : MonoBehaviour
{
    // The index of the current scene in the Build Settings
    private int currentSceneIndex;

    private void Start()
    {
        // Get the index of the current scene
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        // Check if the space bar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Load the next scene in the build
            LoadNextScene();
        }
    }

    private void LoadNextScene()
    {
        // Calculate the index of the next scene in the build
        int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

        // Load the next scene
        SceneManager.LoadScene(nextSceneIndex);
    }
}
