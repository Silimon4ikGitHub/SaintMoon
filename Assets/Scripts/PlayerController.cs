
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    [SerializeField] private GameObject playerHead;
    [SerializeField] private FixedJoystick joystick;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        MoveByButtoms();
        MoveByJoystic();
        RotateByButtons();
        RotateByJoystick();
    }


    private void MoveByButtoms()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 upVector = new Vector3(1, 0, 1);
        Vector3 rightVector = new Vector3(-1, 0, 1);
        Vector3 movement = (rightVector * verticalInput + upVector * horizontalInput).normalized * moveSpeed;

        if (horizontalInput != 0 || verticalInput != 0)
            rb.velocity = movement;
    }
    private void RotateByButtons()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);

        if (horizontalInput != 0 || verticalInput != 0)
            playerHead.transform.rotation = Quaternion.LookRotation(upVector * horizontalInput + rightVector * -verticalInput);
    }

    private void MoveByJoystic()
    {
        float joystickX = joystick.Horizontal;
        float joystickY = joystick.Vertical;

        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);
        Vector3 movement = (rightVector * joystickX + upVector * joystickY).normalized * moveSpeed;

        if (joystickX != 0 || joystickY != 0)
            rb.velocity = movement;
    }

    private void RotateByJoystick()
    {
        float horizontalInput = joystick.Horizontal;
        float verticalInput = joystick.Vertical;
        Vector3 upVector = new Vector3(-1, 0, 1);
        Vector3 rightVector = new Vector3(1, 0, 1);

        if(horizontalInput != 0 || verticalInput != 0)
            playerHead.transform.rotation = Quaternion.LookRotation(upVector * horizontalInput + rightVector * -verticalInput);
    }
}
