using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class TrackPortal : MonoBehaviour
{
    [SerializeField] private CinemachineSmoothPath path;
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private GameObject portal;
    [Space]
    [SerializeField] private float distanceToSpawnPortal;
    [SerializeField] private string scene;

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
                GameObject spawnedPortal = Instantiate(portal, path.EvaluatePosition(17), new Quaternion(0,0,0,0));
                spawnedPortal.transform.rotation = path.EvaluateOrientation(17);
                triggered = true;
            }
        }
        else if(dollyCart.m_Position >= 17)
        {
            SceneManager.LoadScene(scene);
        }
    }
}
