using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TrackPortal : MonoBehaviour
{
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private GameObject portal;
    [Space]
    [SerializeField] private float distanceToSpawnPortal;

    private bool triggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!triggered)
        {
            if (dollyCart.m_Position >= distanceToSpawnPortal)
            {
                GameObject spawnedPortal = Instantiate(portal, path.m_Waypoints[path.m_Waypoints.Length - 1].position, new Quaternion(0,0,0,0));
                spawnedPortal.transform.rotation = path.m_Waypoints[path.m_Waypoints.Length - 1].
            }
        }
    }
}
