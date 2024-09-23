using UnityEngine;

public class RotateMenuTube : MonoBehaviour
{
    private float speed = -10f;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.fixedDeltaTime);
    }
}