using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FirstCutScene : MonoBehaviour
{
    [SerializeField] private CinemachineDollyCart dollyCart;
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 doorEndPos;
    [SerializeField] private float timeTakesDoorToFall = 0.1f;
    [SerializeField] private float distanceAlongSplineDoor = 5;
    [SerializeField] private float distanceAlongSplineEnd = 7;
    [SerializeField] private Transform player;
    [SerializeField] private Transform[] fallingWalls;
    [SerializeField] private float delayBetweenWallsFall = 0.5f;
    [SerializeField] private float wallsEndHeight = 5;
    [SerializeField] private float timeTakesWallToFall = 0.1f;
    [Space]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip wallHitGroundSound;

    private bool doorClose = false;

    private void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 1)
        {
            PlayerMovement.CanMove = false;
        }
        if (dollyCart.m_Position >= distanceAlongSplineDoor && !doorClose)
        {
            doorClose = true;
            StartCoroutine(DoorClose());
        }
        else if(dollyCart.m_Position >= distanceAlongSplineEnd && !PlayerMovement.CanMove)
        {
            PlayerMovement.CanMove = true;
            player.SetParent(null);
            StartCoroutine(WallsFalling());
        }
    }

    IEnumerator DoorClose()
    {
        Vector3 stepPerSec = (doorEndPos - door.position) / timeTakesDoorToFall;
        
        for (float i = 0; i < timeTakesDoorToFall; i += Time.deltaTime)
        {
            door.Translate(stepPerSec * Time.deltaTime);
            yield return null;
        }
        door.position = doorEndPos;
        audioSource.PlayOneShot(wallHitGroundSound);
    }

    IEnumerator WallsFalling()
    {
        yield return new WaitForSeconds(delayBetweenWallsFall);
        Vector3 stepPerSec = (new Vector3(fallingWalls[0].position.x, wallsEndHeight, fallingWalls[0].position.z) - fallingWalls[0].position) / timeTakesDoorToFall;

        for (float i = 0; i < timeTakesDoorToFall; i += Time.deltaTime)
        {
            fallingWalls[0].Translate(stepPerSec * Time.deltaTime);
            yield return null;
        }
        fallingWalls[0].position = new Vector3(fallingWalls[0].position.x, wallsEndHeight, fallingWalls[0].position.z);
        audioSource.PlayOneShot(wallHitGroundSound);

        Vector3[] stepsPerSec = new Vector3[fallingWalls.Length - 1];
        for (int w = 0; w < fallingWalls.Length - 1; w++)
        {
            stepsPerSec[w] = (new Vector3(fallingWalls[w + 1].position.x, wallsEndHeight, fallingWalls[w + 1].position.z) - fallingWalls[w+ 1].position) / timeTakesWallToFall;
        }

        for (float i = 0; i < timeTakesWallToFall * fallingWalls.Length - 1; i += Time.deltaTime)
        {
            for (int w = 1; w < fallingWalls.Length; w++)
            {
                if (fallingWalls[w - 1].position.y + 1 < fallingWalls[w].position.y || fallingWalls[w - 1].position.y <= wallsEndHeight)
                {
                    fallingWalls[w].Translate(stepsPerSec[w - 1] * Time.deltaTime);
                }

                if (fallingWalls[w].position.y < wallsEndHeight)
                {
                    fallingWalls[w].position = new Vector3(fallingWalls[w].position.x, wallsEndHeight, fallingWalls[w].position.z);
                }
            }
            yield return null;
        }
    }
}
