using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ScoreScriptableObject : ScriptableObject
{
    public float Score;
    public float Kills;

    public void AddScore(float value)
    {
        Score += value;
    }

    public void AddKill(float value)
    {
        Kills += value;
    }
}
