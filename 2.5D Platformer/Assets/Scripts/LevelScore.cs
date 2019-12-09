using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelScore : MonoBehaviour
{
    [SerializeField] HighScoreLevel highScoreLevel;
    [SerializeField] float timeLeftSec = 300;
    [SerializeField] string name = "marc";
    [SerializeField] UnityEvent OnTimeUp = new UnityEvent();

    public void SetScore()
    {
        highScoreLevel.AddScore(name, timeLeftSec);
    }

    private void Update()
    {
        timeLeftSec -= Time.deltaTime;
        if (timeLeftSec <= 0)
        {
            OnTimeUp.Invoke();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        SetScore();
    }
}
