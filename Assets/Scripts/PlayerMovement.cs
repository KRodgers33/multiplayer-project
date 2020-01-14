using UnityEngine;
using Mirror;

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

    void Update()
    {
        if (!isLocalPlayer) { return; }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            controller.slopeLimit = 45.0f;
            velocity.y = -9.81f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        Vector3 move = transform.right * x + transform.forward * z;
        float magnitude = Mathf.Clamp01(move.magnitude);
        move = Vector3.Normalize(move) * magnitude;

        controller.Move(move * moveSpeed * Time.deltaTime);
       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            controller.slopeLimit = 100.0f;
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if ((controller.collisionFlags & CollisionFlags.Above) != 0)
        {
            velocity.y = -9.81f;
        }
    }
}
