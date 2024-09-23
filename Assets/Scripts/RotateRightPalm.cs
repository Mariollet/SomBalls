using UnityEngine;

public class RotateRightPalm : MonoBehaviour
{
    private float speed = 60f;

    void FixedUpdate()
    {
        transform.Rotate(0, -speed * Time.fixedDeltaTime, 0);
    }
}