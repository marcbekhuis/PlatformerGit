using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnPlayerDie = new UnityEvent();

    [Header("Health Icons")]
    [SerializeField] private GameObject healthIcon;
    [SerializeField] private Transform healthIconHolder;

    private PlayerMovement playerMovement;
    private List<GameObject> placedHealthIcons = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        AddHealthIcon(health);
    }

    public void RemoveHealth(int lost)
    {
        RemoveHealthIcon(lost);
        if (health - lost <= 0)
        {
            health = 0;
            PlayerMovement.CanMove = false;
            OnPlayerDie.Invoke();
        }
        else
        {
            health -= lost;
        }
    }

    private void AddHealthIcon(int amountToAdd)
    {
        for (int i = 0; i < amountToAdd; i++)
        {
            placedHealthIcons.Add(Instantiate(healthIcon, healthIconHolder));
        }
    }

    private void RemoveHealthIcon(int amountToRemove)
    {
        for (int i = 0; i < amountToRemove && placedHealthIcons.Count > 0; i++)
        {
            Destroy(placedHealthIcons[placedHealthIcons.Count - 1]);
            placedHealthIcons.RemoveAt(placedHealthIcons.Count - 1);
        }
    }
}
