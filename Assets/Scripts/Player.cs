using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] int currentHP;
    public int CurrentHP { get => currentHP; set => currentHP = value; }
    [SerializeField] float invincibilityDuration;
    [SerializeField] bool isInvincible;

    [Header("Death Settings")]
    [SerializeField] float respawnDuration;
    [SerializeField] float deathDuration;

    [Header("Movement Settings")]
    [SerializeField] float currentSpeed;
    [SerializeField] float walkSpeed = 12f;
    [SerializeField] float shiftWalkSpeed;
    [SerializeField] float fallSpeed = 12f;
    [SerializeField] float shiftFallSpeed;
    [SerializeField] float moveTransitionDuration;
    [SerializeField] float jumpHeight = 3f;
    [SerializeField] float gravity = -9.81f;
    [SerializeField] float groundDistance = 0.4f;
    [SerializeField] LayerMask groundMask;

    Vector3 inputs;
    Vector3 velocity;
    public bool isGrounded;
    public bool isFalling;
    public bool isDead;
    bool hasFallenAlready;
    bool inTransition;

    [Header("References")]
    [SerializeField] Transform respawnPoint;
    [SerializeField] CharacterController charController;
    [SerializeField] Rigidbody rb;
    [SerializeField] Transform groundCheck;
    [SerializeField] UIManager uiManager;
    [SerializeField] FPSCamera camManager;

    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        Movement();
    }

    private void FixedUpdate()
    {
        if (!isDead)
            rb.MovePosition(rb.position + inputs * currentSpeed * Time.fixedDeltaTime);
    }

    void Movement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // movement
        inputs.x = Input.GetAxis("Horizontal");
        inputs.z = Input.GetAxis("Vertical");
        inputs = transform.TransformDirection(inputs);

        if (!inTransition)
        {
            if (!isFalling)
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    currentSpeed = shiftWalkSpeed;
                else
                    currentSpeed = walkSpeed;
            }
            else
            {
                if (Input.GetKey(KeyCode.LeftShift))
                    currentSpeed = shiftFallSpeed;
                else
                    currentSpeed = fallSpeed;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        } 
    }

    void CharacterControllerMovement()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2;
        }

        // movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        if (!isFalling)
        {
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = shiftWalkSpeed;
            else
                currentSpeed = walkSpeed;
        }
        else
        {
            if (Input.GetKey(KeyCode.LeftShift))
                currentSpeed = shiftFallSpeed;
            else
                currentSpeed = fallSpeed;
        }

        charController.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // apply gravity
        velocity.y += gravity * Time.deltaTime;

        charController.Move(velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "FallTrigger")
        {
            print("Falling");
            if (!hasFallenAlready)
            {
                uiManager.StartFallingDialogue();
                hasFallenAlready = true;
            }

            isFalling = true;
            StartCoroutine(WalkToFallSpeedTransitonCoroutine());
        }
        else if (other.gameObject.tag == "EndTrigger")
        {
            uiManager.EndGameFade(1);
        }
    }

    IEnumerator WalkToFallSpeedTransitonCoroutine()
    {
        inTransition = true;
        float elapsed = 0;

        while (elapsed < moveTransitionDuration)
        {
            currentSpeed = Mathf.Lerp(walkSpeed, fallSpeed, elapsed / moveTransitionDuration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        inTransition = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle" && !isInvincible)
        {
            print("Take Damage");
            TakeDamage();
            StartCoroutine(InvincibilityCoroutine());
        }
    }

    IEnumerator InvincibilityCoroutine()
    {
        isInvincible = true;

        yield return new WaitForSeconds(invincibilityDuration);

        isInvincible = false;
    }

    void TakeDamage()
    {
        currentHP--;
        uiManager.TakeDamage(currentHP);

        // TODO ADD CAMERA FLASH HERE

        if (currentHP <= 0)
        {
            print("Death");
            Die();
        }
    }

    void Die()
    {
        uiManager.DeathFade(deathDuration, respawnDuration);
    }

    public void Respawn()
    {
        transform.position = respawnPoint.position;
    }
}
