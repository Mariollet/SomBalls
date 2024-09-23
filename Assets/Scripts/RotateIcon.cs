using UnityEngine;

public class RotateIcon : MonoBehaviour
{
    private float speed = 320f;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}