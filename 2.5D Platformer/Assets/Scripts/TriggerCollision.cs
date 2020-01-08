using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerCollision : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTrigger = new UnityEvent();
    [SerializeField] private float Cooldown = 0;

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad > Cooldown)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnTrigger.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Time.timeSinceLevelLoad > Cooldown)
        {
            if (other.CompareTag("Player"))
            {
                OnTrigger.Invoke();
            }
        }
    }
}
