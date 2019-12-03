using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldownSec = 0.1f;

    private float delay = 0;

    private void OnTriggerStay(Collider collision)
    {
        if (delay < Time.time && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().RemoveHealth(damage);
            delay = Time.time + cooldownSec;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (delay < Time.time && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponentInParent<PlayerHealth>().RemoveHealth(damage);
            delay = Time.time + cooldownSec;
        }
    }
}
