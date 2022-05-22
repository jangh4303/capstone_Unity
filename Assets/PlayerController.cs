using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerController : MonoBehaviourPunCallbacks
{
	[SerializeField] GameObject cameraHolder;
	[SerializeField] GameObject ui;
	[SerializeField] float mouseSensitivity, jumpForce, smoothTime;

	
	

	public float runSpeed;
    public float speed;

	/*[Tooltip("The height the player can jump")]
    public float JumpHeight = 1.2f;

    [Tooltip("Time required to pass before being able to jump again. Set to 0f to instantly jump again")]
    public float JumpTimeout = 0.50f;

    [Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
    public float FallTimeout = 0.15f;

    [Header("Player Grounded")]
    [Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
    public bool Grounded = true;

    [Tooltip("Useful for rough ground")]
    public float GroundedOffset = -0.14f;

    [Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
    public float GroundedRadius = 0.28f;

    [Tooltip("What layers the character uses as ground")]
    public LayerMask GroundLayers;*/

	[Tooltip("Time required to pass before entering the fall state. Useful for walking down stairs")]
	public float FallTimeout = 0.15f;
	private float _fallTimeoutDelta;
	float hAxis;
    float vAxis;
	float verticalLookRotation;
	bool grounded;
	bool freefall = false;
	bool runKeyDown;
    bool jumpKeyDown;

	Vector3 smoothMoveVelocity;
	Vector3 moveAmount;
	Vector3 moveVec;
    Animator anim;
    PhotonView PV;
	Rigidbody rb;
	PlayerManager playerManager;

	void Awake()
    {
        anim = GetComponentInChildren<Animator>();
      
		rb = GetComponent<Rigidbody>();
		PV = GetComponent<PhotonView>();

		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;

		//playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();

	}

    void Start()
    {
        if (!PV.IsMine)     // 다른 사용자와 카메라 곂치는걸 막음 PV다르면 그내부 컴포넌트 파괴
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
			Destroy(rb);
			Destroy(ui);
		
		}
		_fallTimeoutDelta = FallTimeout;
	}

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
		anim.SetBool("IsWalk", moveVec != Vector3.zero);
		anim.SetBool("IsRun", runKeyDown);
		anim.SetBool("Jump", jumpKeyDown);
		anim.SetBool("Grounded", grounded);
		anim.SetBool("FreeFall", freefall);

		Look();
		Move();
		Jump();


		if (transform.position.y < -10f) // Die if you fall out of the world
		{
			Die();
		}
		
       
	}
	void Look()
	{
		transform.Rotate(Vector3.up * Input.GetAxisRaw("Mouse X") * mouseSensitivity);

		verticalLookRotation += Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90f, 70f);

		cameraHolder.transform.localEulerAngles = Vector3.left * verticalLookRotation;
	}

	void Move()
	{
		hAxis = Input.GetAxisRaw("Horizontal");
		vAxis = Input.GetAxisRaw("Vertical");
		runKeyDown = Input.GetButton("Run");
		

		moveVec = new Vector3(hAxis, 0, vAxis).normalized;

		moveAmount = Vector3.SmoothDamp(moveAmount, moveVec * (runKeyDown ? runSpeed : speed), ref smoothMoveVelocity, smoothTime);
	}

	void Jump()
	{
		jumpKeyDown = Input.GetButton("Jump");
        if (grounded)
        {
			// reset the fall timeout timer

			_fallTimeoutDelta = FallTimeout;
			freefall = false;
			if (jumpKeyDown)
			{
				rb.AddForce(transform.up * jumpForce);
			}
		}
        else
		{ // fall timeout
			if (_fallTimeoutDelta >= 0.0f)
			{
				_fallTimeoutDelta -= Time.deltaTime;
			}
			else
			{

				freefall = true;

			}
		}

		
	}

	public void SetGroundedState(bool _grounded)
	{
		grounded = _grounded;
	}

	void FixedUpdate()
	{
		if (!PV.IsMine)
			return;

		rb.MovePosition(rb.position + transform.TransformDirection(moveAmount) * Time.fixedDeltaTime);
	}

	void Die()
	{
		playerManager.Die();
	}
}
