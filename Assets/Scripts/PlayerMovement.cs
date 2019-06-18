using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float sidewaysForce = 700f;
    public float stopPos = 0.25f;
    public GameManager manager;

    private void FixedUpdate()
    {
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") > 0)
        {
            rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }
        if (Input.GetButton("Horizontal") && Input.GetAxisRaw("Horizontal") < 0)
        {
            rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
        }

        var middleOfScreen = Screen.currentResolution.width / 2;

        if (Input.touchCount > 0)
        {
            var touch = Input.GetTouch(0);

            if (touch.position.x > middleOfScreen)
            {
                rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }

            if (touch.position.x < middleOfScreen)
            {
                rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
        }

        if (rb.position.y < stopPos)
        {
            manager.GameOver();
        }
    }
}
