using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMoveMent playerMovement;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMoveMent>();

    }

    private void Update()
    {
        // x,z 방향 이동
        float x = Input.GetAxisRaw("Horizontal");       //방향키 좌/우 움직임
        float z = Input.GetAxisRaw("Vertical");         // 방향키 위/아래 움직임

        playerMovement.MoveTo(new Vector3(x, 0, z));
    }
}
