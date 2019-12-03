using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerHealth : MonoBehaviour
{
    public static bool PlayerAlive = true;

    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnPlayerDie = new UnityEvent();

    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    public void RemoveHealth(int lost)
    {
        if (health - lost <= 0)
        {
            health = 0;
            PlayerAlive = false;
            OnPlayerDie.Invoke();
        }
        else
        {
            health -= lost;
        }
    }
}
