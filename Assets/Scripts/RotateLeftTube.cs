using UnityEngine;

public class RotateLeftTube : MonoBehaviour
{
    private float speed = 25f;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}