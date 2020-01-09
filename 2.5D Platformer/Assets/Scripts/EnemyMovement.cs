using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public LayerMask ground;
    public Vector3 groundCheckSize;
    public Vector3 groundCheckOffsetLeft;
    public Vector3 groundCheckOffsetRight;

    public Vector3 wallCheckSize;
    public Vector3 wallCheckOffsetLeft;
    public Vector3 wallCheckOffsetRight;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
        rigidbody.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
    }

    // Update is called once per frame
    void Update()
    {
        if (!Physics.CheckBox(this.transform.position + groundCheckOffsetRight, groundCheckSize * 0.5f, new Quaternion(0,0,0,0), ground))
        {
            speed *= -1;
        }
        else if (!Physics.CheckBox(this.transform.position + groundCheckOffsetLeft, groundCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), ground))
        {
            speed *= -1;
        }
        else if (Physics.CheckBox(this.transform.position + wallCheckOffsetLeft, wallCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), ground))
        {
            speed *= -1;
        }
        else if (Physics.CheckBox(this.transform.position + wallCheckOffsetRight, wallCheckSize * 0.5f, new Quaternion(0, 0, 0, 0), ground))
        {
            speed *= -1;
        }
        rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y,0);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(this.transform.position + groundCheckOffsetLeft, groundCheckSize);
        Gizmos.DrawCube(this.transform.position + groundCheckOffsetRight, groundCheckSize);
        Gizmos.color = new Color(0, 1, 0, 0.5f);
        Gizmos.DrawCube(this.transform.position + wallCheckOffsetLeft, wallCheckSize);
        Gizmos.DrawCube(this.transform.position + wallCheckOffsetRight, wallCheckSize);
    }
}
