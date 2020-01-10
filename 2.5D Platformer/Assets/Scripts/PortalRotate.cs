using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRotate : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0,speed * Time.deltaTime,0), Space.Self);
    }
}
