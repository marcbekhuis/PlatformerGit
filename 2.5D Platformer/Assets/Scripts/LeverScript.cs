using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverScript : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjects;

    private bool overlapping = false;

    // Update is called once per frame
    void Update()
    {
        if (overlapping && Input.GetKeyDown(KeyCode.E))
        {
            foreach (var item in gameObjects)
            {
                item.SetActive(true);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overlapping = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            overlapping = false;
        }
    }
}
