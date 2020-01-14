using UnityEngine;
using Mirror;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu = null;

    private void Update()
    {
        // Lock or unlock mouse on key P
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

        // Check if local client has a player active
        bool localPlayerActive = false;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<NetworkIdentity>().hasAuthority == true) localPlayerActive = true;
        }

        // Pause game if mouse isn't locked and local client is in game
        if (Cursor.lockState != CursorLockMode.Locked && localPlayerActive)
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
