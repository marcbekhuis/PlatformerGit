using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{

    [SerializeField] private int health = 3;
    [SerializeField] private UnityEvent OnPlayerDie = new UnityEvent();
    [Space]
    [SerializeField] private float animationTimeSec = 1;

    private BoxCollider2D playerCollider;
    private GameObject SpriteChild;
    private PlayerMovement playerMovement;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = this.GetComponent<BoxCollider2D>();
        SpriteChild = this.transform.Find("Sprite").gameObject;
        playerMovement = this.GetComponent<PlayerMovement>();
    }

    public void RemoveHealth(int lost)
    {
        if (health - lost <= 0)
        {
            health = 0;
            OnPlayerDie.Invoke();
        }
        else
        {
            health -= lost;
            StartCoroutine(SizeDown(SpriteChild.transform.localScale.x - 0.5f));
        }
    }

    private IEnumerator SizeDown(float newSize)
    {
        float SizeDownPerSec = (SpriteChild.transform.localScale.x - newSize) / animationTimeSec;
        for (float i = 0; i < animationTimeSec; i += Time.deltaTime)
        {
            SpriteChild.transform.localScale -= new Vector3(SizeDownPerSec, SizeDownPerSec, 0) * Time.deltaTime;
            playerCollider.size -= new Vector2(SizeDownPerSec, SizeDownPerSec) / 2 * Time.deltaTime;
            yield return null;
        }
        SpriteChild.transform.localScale = new Vector3(newSize, newSize, 1);
        playerCollider.size = new Vector2(newSize, newSize) / 2;
        playerMovement.groundCheckHeight += 0.125f;
    }
}
