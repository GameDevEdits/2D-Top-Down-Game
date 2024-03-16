using UnityEngine;
using UnityEngine.SceneManagement;

public class BackPreviousScene : MonoBehaviour
{
    private int previousSceneIndex;
    private int currentSceneIndex;

    private void Awake()
    {
        // Don't destroy this object when loading new scenes
        DontDestroyOnLoad(gameObject);

        // Save the initial scene index
        previousSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnEnable()
    {
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Unsubscribe from scene loaded event
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Update the previous scene index to the current scene index
        previousSceneIndex = currentSceneIndex;

        // Save the current scene index
        currentSceneIndex = scene.buildIndex;
    }

    public void GoBackToPreviousScene()
    {
        // Load the previous scene
        SceneManager.LoadScene(previousSceneIndex);
    }
}
