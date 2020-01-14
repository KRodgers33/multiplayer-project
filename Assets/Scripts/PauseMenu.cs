using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;

    private void Update()
    {
        // lock or unlock mouse on key P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else if (GameObject.FindGameObjectWithTag("Player") != null)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // Allow cursor if game inactive (if player is gone)
        if (Cursor.lockState != CursorLockMode.Locked && GameObject.FindGameObjectWithTag("Player") != null)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }

        // hide cursor if locked
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
    }
}
