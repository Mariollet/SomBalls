using System.Collections;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public GameManager gameManager;

    void FixedUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}