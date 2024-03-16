using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToIndexScene : MonoBehaviour
{
    public void GoToEnemyIndexScene()
    {
        // Load the scene named "EnemyIndex"
        SceneManager.LoadScene("EnemyIndex");
    }
}
