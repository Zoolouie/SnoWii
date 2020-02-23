using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPCharacterController : MonoBehaviour
{
    public float speed;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate ()
    {

        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        // Rotate the cube by converting the angles into a quaternion.
        Quaternion target = Quaternion.Euler(0, moveHorizontal * 45, 0);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target,  Time.deltaTime * 5.0f);
        Vector3 movement = new Vector3 (0.0f, 0.0f, -moveVertical);
        Debug.Log(rb.velocity[1]);
        if (rb.velocity[1] < -15) {
            rb.AddRelativeForce(movement * -speed);
        }
        rb.AddRelativeForce (movement * speed);
        if (rb.velocity.y > 1) {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        }
    }
}
