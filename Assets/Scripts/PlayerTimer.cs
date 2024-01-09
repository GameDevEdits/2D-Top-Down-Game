using UnityEngine;
using TMPro;

public class PlayerTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;   // Reference to the TextMeshProUGUI component for displaying the timer.
    public TextMeshProUGUI killCountText; // Reference to the TextMeshProUGUI component for displaying the kill count.

    private float startTime; // Time when the player starts the game.
    private int killCount;   // Number of kills.

    private void Start()
    {
        // Initialize the timer and kill count when the game starts.
        startTime = Time.time;
        killCount = 0;
        UpdateKillCountText();
    }

    private void Update()
    {
        // Calculate the elapsed time.
        float elapsedTime = Time.time - startTime;

        // Format the time in minutes and seconds.
        string minutes = ((int)elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");

        // Update the timer display.
        timerText.text = $"{minutes}:{seconds}";
    }

    // Method to increment the kill count.
    public void IncrementKillCount()
    {
        killCount++;
        UpdateKillCountText();
    }

    // Method to update the kill count display.
    private void UpdateKillCountText()
    {
        killCountText.text = $"Kills: {killCount}";
    }
}
