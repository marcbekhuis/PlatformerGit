using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class SealOpening : MonoBehaviour
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
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip sealMovingSound;

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
                    //this.transform.localEulerAngles = rotation.rotation;
                    StartCoroutine(Rotate(rotation.blendDuration, rotation.rotation));
                    rotation.triggered = true;
                }
            }
        }
    }

    IEnumerator Rotate(float lenghtSec, Vector3 rotation)
    {
        Vector3 rotationPerSec = (rotation - this.transform.localEulerAngles) / lenghtSec;
        if (audioSource && sealMovingSound)
        {
            audioSource.PlayOneShot(sealMovingSound);
        }
        for (float i = 0; i < lenghtSec; i += Time.deltaTime)
        {
            this.transform.localEulerAngles += rotationPerSec * Time.deltaTime;
            yield return null;
        }
        this.transform.localEulerAngles = rotation;
        if (audioSource && sealMovingSound)
        {
            audioSource.Stop();
        }
    }
}
