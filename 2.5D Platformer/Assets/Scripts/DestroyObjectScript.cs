using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectScript : MonoBehaviour
{
    public void DestroyObject(GameObject toDestroy)
    {
        Destroy(toDestroy);
    }
}
