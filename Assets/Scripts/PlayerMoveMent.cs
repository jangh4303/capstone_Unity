using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f; // �̵� �ӵ�
    private Vector3 moveDirection;  // �̵� ����

    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }


    // ���������� ������ moveDirection�� ����
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
