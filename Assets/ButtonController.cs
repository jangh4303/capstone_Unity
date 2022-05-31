using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    [SerializeField] GameObject player;
    public PlayerController playerScript;

    public void Init()
    {
        playerScript = player.GetComponent<PlayerController>();

    }

    public void JumpUp()
    {
        
        playerScript.jumpKeyDown = false;
    }
    public void JumpDown()
    {
    
        playerScript.jumpKeyDown = true;
    }

    public void RunUp()
    {
        playerScript.runKeyDown = false;
    }
    public void RunDown()
    {
        playerScript.runKeyDown = true;
    }
    public void QuitProgram()
    {
        Application.Quit();
    }
}
