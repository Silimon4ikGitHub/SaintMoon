using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed at which the player moves
    private Rigidbody rb; // Reference to the player's Rigidbody component
    [SerializeField] private float offset;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private FixedJoystick joystick;


    private void Start()
    {
        // Get the player's Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveByButtoms();
        MoveByJoystic();
        RotateByButtons();
        RotateByJoystick();
    }
    private void RotateByButtons()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);

        playerHead.transform.rotation = Quaternion.LookRotation(upVector * horizontalInput + rightVector * -verticalInput);
    }

    private void MoveByButtoms()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 upVector = new Vector3(1, 0, 1);
        Vector3 rightVector = new Vector3(-1, 0, 1);
        Vector3 movement = (rightVector * verticalInput + upVector * horizontalInput).normalized * moveSpeed;

        rb.velocity = movement;
    }

    private void MoveByJoystic()
    {
        float joystickX = joystick.Horizontal;
        float joystickY = joystick.Vertical;

        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);
        Vector3 movement = (rightVector * joystickX + upVector * joystickY).normalized * moveSpeed;

        rb.velocity = movement;
    }

    private void RotateByJoystick()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);

        playerHead.transform.rotation = Quaternion.LookRotation(upVector * horizontalInput + rightVector * -verticalInput);
    }
}
