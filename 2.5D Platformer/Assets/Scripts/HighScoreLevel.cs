using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScore Level *", menuName = "ScriptableObjects/HighScore Level")]
public class HighScoreLevel : ScriptableObject
{
    private Dictionary<string, Score> scores = new Dictionary<string, Score>();

    public struct Score
    {
        public Score(float TimeLeft)
        {
            timeLeft = TimeLeft;
        }

        public float timeLeft;
    }

    public void AddScore(string playerName = "Empty", float timeLeft = 0)
    {
        if (!scores.ContainsKey(playerName))
        {
            scores.Add(playerName, new Score(timeLeft));
        }
        else if(scores[playerName].timeLeft < timeLeft)
        {
            scores.Remove(playerName);
            scores.Add(playerName, new Score(timeLeft));
        }
    }

    public Dictionary<string, Score> GetHighScore(int numberOfScores)
    {
        Dictionary<string, Score> tempScores = new Dictionary<string, Score>(scores);
        Dictionary<string, Score> highScoreNames = new Dictionary<string, Score>();

        for (int i = 0; i < numberOfScores; i++)
        {
            if (tempScores.Count > 0)
            {
                float highestTime = 0;
                string highestName = "";
                foreach (var score in tempScores)
                {
                    if (score.Value.timeLeft > highestTime)
                    {
                        highestTime = score.Value.timeLeft;
                        highestName = score.Key;
                    }
                }
                tempScores.Remove(highestName);
                highScoreNames.Add(highestName, scores[highestName]);
            }
            else
            {
                break;
            }
        }
        return highScoreNames;
    }
}
