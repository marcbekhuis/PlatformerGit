using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagingObject : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float cooldownSec = 0.1f;

    private float delay = 0;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (delay < Time.time && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().RemoveHealth(damage);
            delay = Time.time + cooldownSec;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (delay < Time.time && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().RemoveHealth(damage);
            delay = Time.time + cooldownSec;
        }
    }
}
