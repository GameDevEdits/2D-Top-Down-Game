using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;

    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    public void LoadPreviousLevel()
    {
        // Pass the scene name or index to LoadLevel directly
        StartCoroutine(LoadLevel("Main Menu"));
    }

    public void GoToRoom1Scene()
    {
        StartCoroutine(LoadLevel("Room 1"));
    }

    public void GoToEnemyIndex()
    {
        StartCoroutine(LoadLevel("EnemyIndex"));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        //Play Anim
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //LoadScene
        SceneManager.LoadScene(levelIndex);
    }

    IEnumerator LoadLevel(string sceneName)
    {
        //Play Anim
        transition.SetTrigger("Start");

        //Wait
        yield return new WaitForSeconds(transitionTime);

        //LoadScene by name
        SceneManager.LoadScene(sceneName);
    }
}
