using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScore : MonoBehaviour
{
    [SerializeField] HighScoreLevel highScoreLevel;
    [SerializeField] float timeLeft = 1;
    [SerializeField] string name = "marc";

    public void SetScore()
    {
        highScoreLevel.AddScore(name, timeLeft);
    }
}
