using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveMent : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f; // 이동 속도
    private Vector3 moveDirection;  // 이동 방향

    private CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
    }


    // 방향정보를 가져와 moveDirection에 저장
    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }
}
