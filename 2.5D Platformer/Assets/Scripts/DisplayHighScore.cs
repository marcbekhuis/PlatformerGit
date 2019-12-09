using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHighScore : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private int amountOfScoresDisplayed = 3;

    public void DisplayScore(HighScoreLevel highScoreLevel)
    {
        scoreText.text = "";
        foreach (var score in highScoreLevel.GetHighScore(3))
        {
            scoreText.text += score.Key + " - Time Left: " + score.Value.timeLeft + "\n";
        }
    }
}
