using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Rigidbody rb;
    public float speed = 2000f;

    private void FixedUpdate()
    {
        rb.AddForce(0, 0, -speed * Time.deltaTime);
    }
}
