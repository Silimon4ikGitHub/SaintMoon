using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    private Rigidbody rb; // Reference to the player's Rigidbody component
    [SerializeField] private float offset;
    [SerializeField] private GameObject playerHead;

    private void Start()
    {
        // Get the player's Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // Get input from the horizontal and vertical axes
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 upVector = new Vector3 (1, 0, 1);
        Vector3 rightVector = new Vector3(-1, 0, 1);
        Vector3 movement = (rightVector * verticalInput + upVector * horizontalInput).normalized * moveSpeed;

        // Apply the movement vector to the player's Rigidbody component
        rb.velocity = movement;
    }
}
