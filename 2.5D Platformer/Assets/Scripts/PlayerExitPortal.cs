using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExitPortal : MonoBehaviour
{

    [SerializeField] float walkDuration = 3;

    private PlayerSideViewMovement playerSideViewMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerSideViewMovement = this.GetComponent<PlayerSideViewMovement>();
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        yield return null;
        PlayerMovement.CanMove = false;
        for (float i = 0; i < walkDuration; i+= Time.deltaTime)
        {
            playerSideViewMovement.Movement(1);
            yield return null;
        }
        PlayerMovement.CanMove = true;
    }
}
