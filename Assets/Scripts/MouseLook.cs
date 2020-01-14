using UnityEngine;
using Mirror;
using System.Collections;

public class MouseLook : NetworkBehaviour
{
    public float mouseSensitivity = 100f;

    [SerializeField] Transform playerBody = null;

    float xRotation = 0f;
    
    IEnumerator Start()
    {
        // Wait to see if object belongs to you or not
        yield return new WaitForEndOfFrame();

        // Disable object if camera doesnt belong to you
        if (!transform.parent.GetComponent<NetworkIdentity>().hasAuthority) { gameObject.SetActive(false); }
    }

    void Update()
    {
        // Don't do anything if the game is paused
        if (Cursor.lockState != CursorLockMode.Locked || !transform.parent.GetComponent<NetworkIdentity>().hasAuthority) { return; }

        // Get mouse inputs
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Do some math
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Update camera position
        playerBody.Rotate(Vector3.up * mouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }
}
