using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 1;
    public int damage = 1;
    public LayerMask player;
    public Vector3 wallCheckSize;
    public Vector3 wallCheckOffsetLeft;
    public Vector3 wallCheckOffsetRight;

    public Vector3 damageCheckSize;
    public Vector3 damageCheckOffset;

    private bool allowDamaging = true;
    private bool allowDamage = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] players;
        if ((players = Physics.OverlapBox(this.transform.position + wallCheckOffsetLeft, wallCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), player)).Length > 0)
        {
            if (allowDamaging)
            {
                foreach (var item in players)
                {
                    item.GetComponentInParent<PlayerHealth>().RemoveHealth(damage);
                }
                allowDamaging = false;
            }
        }
        else if ((players = Physics.OverlapBox(this.transform.position + wallCheckOffsetRight, wallCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), player)).Length > 0)
        {
            if (allowDamaging)
            {
                foreach (var item in players)
                {
                    item.GetComponentInParent<PlayerHealth>().RemoveHealth(damage);
                }
                allowDamaging = false;
            }
        }
        else
        {
            allowDamaging = true;
        }

        if ((players = Physics.OverlapBox(this.transform.position + damageCheckOffset, damageCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), player)).Length > 0)
        {
            if (allowDamage)
            {
                foreach (var item in players)
                {
                    if (health - item.GetComponentInParent<PlayerHealth>().damage <= 0)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        health -= item.GetComponentInParent<PlayerHealth>().damage;
                        item.GetComponentInParent<Rigidbody>().AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
                    }
                }
                allowDamage = false;
            }
        }
        else
        {
            allowDamage = true;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(this.transform.position + damageCheckOffset, damageCheckSize);
    }
}
