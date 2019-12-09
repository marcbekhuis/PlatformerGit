using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Portal : MonoBehaviour
{
    [SerializeField] private UnityEvent OnPortalEnter = new UnityEvent();

    private void OnCollisionEnter(Collision collision)
    {
        if (Time.timeSinceLevelLoad > 1)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                OnPortalEnter.Invoke();
            }
        }
    }
}
