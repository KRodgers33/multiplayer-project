using UnityEngine;
using Mirror;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;

    [SerializeField] Transform playerBody = null;

    float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        if (!transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer) { gameObject.SetActive(false); }
    }

    void Update()
    {
        if (Cursor.lockState != CursorLockMode.Locked || !transform.parent.GetComponent<NetworkIdentity>().isLocalPlayer) { return; }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
