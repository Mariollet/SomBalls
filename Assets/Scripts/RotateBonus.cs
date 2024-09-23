using UnityEngine;

public class RotateBonus : MonoBehaviour
{
    private float speedRotate = 4f;

    private void Start()
    {

    }

    void FixedUpdate()
    {
        transform.Rotate(0, speedRotate, 0);
    }
}
