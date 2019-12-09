using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HighScore Level *", menuName = "ScriptableObjects/HighScore Level")]
public class HighScoreLevel : ScriptableObject
{
    [SerializeField] private List<Score> scores = new List<Score>();

    [System.Serializable]
    public class Score
    {
        public Score(string Name, float TimeLeft)
        {
            name = Name;
            timeLeft = TimeLeft;
        }

        public string name;
        public float timeLeft;
    }

    public void AddScore(string playerName = "None", float timeLeft = 0)
    {
        int index;
        if (!FindName(playerName, out index))
        {
            scores.Add(new Score(playerName, timeLeft));
        }
        else if(scores[index].timeLeft < timeLeft)
        {
            scores[index].timeLeft = timeLeft;
        }
    }

    public List<Score> GetHighScore(int numberOfScores)
    {
        List<Score> tempScores = new List<Score>(scores);
        List<Score> highestScores = new List<Score>();

        for (int i = 0; i < numberOfScores; i++)
        {
            if (tempScores.Count > 0)
            {
                Score highestScore = new Score("None", 0);
                foreach (var score in tempScores)
                {
                    if (score.timeLeft > highestScore.timeLeft)
                    {
                        highestScore = score;
                    }
                }
                tempScores.Remove(highestScore);
                highestScores.Add(highestScore);
            }
            else
            {
                break;
            }
        }
        return highestScores;
    }

    private bool FindName(string name, out int index)
    {
        for (int i = 0; i < scores.Count; i++) 
        {
            if (scores[i].name == name)
            {
                index = i;
                return true;
            }
        }
        index = 0;
        return false;
    }
}
