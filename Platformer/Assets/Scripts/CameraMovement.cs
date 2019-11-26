using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] Transform playerLoc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.position.x < playerLoc.position.x)
        {
            this.transform.position = new Vector3(playerLoc.position.x, playerLoc.position.y, 0);
        }
        else
        {
            this.transform.position = new Vector3(this.transform.position.x, playerLoc.position.y, 0);
        }
    }
}
