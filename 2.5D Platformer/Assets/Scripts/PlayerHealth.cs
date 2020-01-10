using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    public int damage = 1;
    [SerializeField] private UnityEvent OnPlayerDie = new UnityEvent();
    [SerializeField] private GameObject armor;

    [Header("Health Icons")]
    [SerializeField] private GameObject healthIcon;
    [SerializeField] private Transform healthIconHolder;

    private PlayerMovement playerMovement;
    private List<GameObject> placedHealthIcons = new List<GameObject>();

    private int armorUsesLeft = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerMovement = this.GetComponent<PlayerMovement>();
        AddHealthIcon(health);
    }

    public void RemoveHealth(int lost)
    {
        if (armorUsesLeft > 0)
        {
            armorUsesLeft--;
            lost--;
            if (armorUsesLeft <= 0)
            {
                armor.SetActive(false);
            }
        }
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

    public void AddHealth(int amountToAdd)
    {
        health += amountToAdd;
        AddHealthIcon(amountToAdd);
    }

    public void EquipArmor(GameObject armorPickup)
    {
        if (armorUsesLeft <= 0)
        {
            armorUsesLeft = 3;
            armor.SetActive(true);
            Destroy(armorPickup);
        }
    }
}
