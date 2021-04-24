using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] float currentSpeed;
    [SerializeField] float speed = 12f;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    [Header("References")]
    [SerializeField] CharacterController charController;
    [SerializeField] Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Movement();
    }

    private void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            // set to -2 instead of 0 in case we register player being grounded before it actually is
            velocity.y = -2f;
        }

        // movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (Input.GetKey(KeyCode.LeftShift))
            currentSpeed = speed * 2;
        else
            currentSpeed = speed;

        charController.Move(move * currentSpeed * Time.deltaTime);

        // jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);
    }
}
