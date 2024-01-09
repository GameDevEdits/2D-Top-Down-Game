using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;   // Reference to the TextMeshProUGUI component for displaying the score.
    public TextMeshProUGUI killText;   // Reference to the TextMeshProUGUI component for displaying the score.
    public ScoreScriptableObject scoreScriptableObject;

    // Array of enemy GameObjects.
    public GameObject[] enemies = new GameObject[10];

    // Array of scores corresponding to each enemy.
    public int[] scores = new int[10];

    private int score = 0;  // Initial score.

    private void Start()
    {
        scoreScriptableObject.Score = 0;
        scoreScriptableObject.Kills = 0;
        UpdateScoreText();
    }

    // Method to increment the score based on the enemy that died.
    public void IncrementScore(GameObject enemy)
    {
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemy == enemies[i])
            {
                score += scores[i];
                break;
            }
        }

        UpdateScoreText();
    }

    public void Update()
    {
        scoreText.text = $"Score: {scoreScriptableObject.Score.ToString()}";
        killText.text = $"Kills: {scoreScriptableObject.Kills.ToString()}";
    }

    // Method to update the score display.
    private void UpdateScoreText()
    {
        scoreText.text = $"Score: {score}";
    }

    // Call this method when an associated enemy dies.
    public void HandleEnemyDeath(GameObject enemy)
    {
        // Notify the ScoreManager to increment the score based on the enemy that died.
        IncrementScore(enemy);
    }
}