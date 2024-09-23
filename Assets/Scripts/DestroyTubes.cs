using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTubes : MonoBehaviour
{
    public GameManager gameManager;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Level"))
        {
            Destroy(other.gameObject);
        }
    }
}
