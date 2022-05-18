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
        // x,z ���� �̵�
        float x = Input.GetAxisRaw("Horizontal");       //����Ű ��/�� ������
        float z = Input.GetAxisRaw("Vertical");         // ����Ű ��/�Ʒ� ������

        playerMovement.MoveTo(new Vector3(x, 0, z));
    }
}
