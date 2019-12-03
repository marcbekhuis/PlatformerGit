using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementSideView : MonoBehaviour
{
    [SerializeField] Transform playerLoc;

    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        offset = this.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = playerLoc.position + offset;
    }
}
