using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRig : MonoBehaviour
{
    [System.Serializable]
    private class Rotation
    {
        public Vector3 rotation;
        public float distanceAlongTrack = 0;
        [Range(0, 1)] public float marge = 0;
        public float blendDuration = 1;
        public bool triggered = false;
    }

    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private Rotation[] rotations;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (var rotation in rotations)
        {
            if (!rotation.triggered)
            {
                if (dollyCart.m_Position >= rotation.distanceAlongTrack - rotation.marge && dollyCart.m_Position <= rotation.distanceAlongTrack + rotation.marge)
                {
                    this.transform.localEulerAngles = rotation.rotation;
                    //StartCoroutine(Rotate(rotation.blendDuration, rotation.rotation));
                    rotation.triggered = true;
                }
            }
        }
    }

    IEnumerator Rotate(float lenghtSec, Vector3 rotation)
    {
        Vector3 rotationPerSec = new Vector3(Mathf.MoveTowardsAngle(this.transform.localEulerAngles.x, rotation.x, 360), Mathf.MoveTowardsAngle(this.transform.localEulerAngles.y, rotation.y, 360), Mathf.MoveTowardsAngle(this.transform.localEulerAngles.z, rotation.z, 360)) / lenghtSec;
        Debug.LogError(rotationPerSec);
        for (float i = 0; i < lenghtSec; i += Time.deltaTime)
        {
            this.transform.localEulerAngles += rotationPerSec * Time.deltaTime;
            yield return null;
        }
        this.transform.localEulerAngles = rotation;
    }
}
