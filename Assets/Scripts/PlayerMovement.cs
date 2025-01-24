using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 100f;   // Speed at which the player moves forward
    public float rotateSpeed = 360f;  // Speed at which the player rotates

// Start is called before the first frame update
private void Start() {
    rb = GetComponent<Rigidbody2D>();
}

    private void FixedUpdate()
    {
        // Forward movement when W is pressed
        if (Input.GetKey(KeyCode.W))
        {
            // Apply force to move forward in the direction the head is facing (transform.up)
            Vector2 forceDirection = transform.up * moveSpeed;
            rb.AddForce(forceDirection, ForceMode2D.Force);  // Apply the force in the direction the worm is facing
        }

        // Rotate the worm left with A key, right with D key
        if (Input.GetKey(KeyCode.A))
        {
            // Apply torque for counterclockwise rotation (left)
            rb.AddTorque(rotateSpeed * Time.deltaTime, ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            // Apply torque for clockwise rotation (right)
            rb.AddTorque(-rotateSpeed * Time.deltaTime, ForceMode2D.Force);
        }
    }

}