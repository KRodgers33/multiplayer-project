using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/*
    This script is currently not in use.
    I was testing stuff which resulted in an unfixible bug,
    and I'm to lazy to fix it today.

    Also, in all other scripts, hasAuthority can be replaced with isLocalPlayer, while
    this script is not in use.
*/

public class PlayerManager : NetworkBehaviour
{
    // Array to store all the different characters
    [SerializeField] GameObject[] playerCharacters = null;

    void Start()
    {
        // Do nothing if player object is not local player
        if (!isLocalPlayer) { return; }
        
        // Spawn playerCharacter
        CmdSpawnPlayerCharacter();

        // Lock mouse
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Spawn player character on server
    [Command]
    void CmdSpawnPlayerCharacter()
    {
        // Spawn player on local client
        // Chooses random character from array playerCharacters, but you have a GUI to choose
        GameObject go = Instantiate(playerCharacters[Random.Range(0, playerCharacters.Length)]);

        // Spawn player on rest of the clients and give local client the authority
        NetworkServer.Spawn(go, connectionToClient);

        go.GetComponent<PlayerMovement>().owner = gameObject;
    }
}
