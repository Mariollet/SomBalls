using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public GameManager gameManager;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;
    private float bound = 4;
    private float boundY = 6;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (gameManager.gameOver == false)
        {
        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        
            if (transform.position.x > bound)
            {
                transform.position = new Vector3(bound, transform.position.y, transform.position.z);
            }
            if (transform.position.x < -bound)
            {
                transform.position = new Vector3(-bound, transform.position.y, transform.position.z);
            }
            if (transform.position.y > boundY)
            {
                transform.position = new Vector3(transform.position.x, boundY, transform.position.z);
            }
        }
    }
}
