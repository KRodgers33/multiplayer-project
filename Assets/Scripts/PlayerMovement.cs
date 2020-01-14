using UnityEngine;
using Mirror;
using System.Collections;

public class PlayerMovement : NetworkBehaviour
{
    public float moveSpeed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    public float jumpHeight = 3f;

    [SerializeField] CharacterController controller = null;
    [SerializeField] Transform groundCheck = null;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;

    public GameObject owner;

    IEnumerator Start()
    {
        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;

        // Wait to see if object belongs to you
        // If it does not then give it the ground layer
        // This is so you can jump on other players
        yield return new WaitForEndOfFrame();
        if (!hasAuthority) { transform.Find("Multiplayer Hitbox").gameObject.SetActive(true); }
    }

    void Update()
    {
        // If player is not you, do nothing
        if (!hasAuthority) { return; }
        
        // Put player in default layer if it's you
        if (gameObject.layer != 0) gameObject.layer = 0;

        // Check if grounded
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Set slope limit to normal if on ground
        if (isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -9.81f;
        }

        // Get movement inputs
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Normalize movements
        Vector3 move = transform.right * x + transform.forward * z;
        float magnitude = Mathf.Clamp01(move.magnitude);
        move = Vector3.Normalize(move) * magnitude;

        // Move
        controller.Move(move * moveSpeed * Time.deltaTime);

        // Jump if on ground
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Do gravity stuff
        // Time.deltaTime is used twice because gravity on earth uses time squared
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // If player hits their head then reset gravity
        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = -9.81f;
        }
    }
}
