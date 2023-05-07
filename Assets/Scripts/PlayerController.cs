using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
	public ControlType controlType;
	public Joystick joystick;

	public float speed;
	const int maxHealth=10;
	public int currentHealth = maxHealth;
	public TMP_Text healthDisplay;

	public enum ControlType{PC,Android};

	private Rigidbody2D rb;
	private PhotonView PV;
	private PlayerManager playerManager;
	private Vector2 moveInput;
	private Vector2 moveVelocity;
	
	void  Awake()
	{
		rb = GetComponent<Rigidbody2D>();
		PV = GetComponent<PhotonView>();
	}

	void Start()
	{
		if (!PV.IsMine)
			{
			Destroy(GetComponentInChildren<Joystick>().gameObject);
			}
		
	}

	void Update()
	{
		if (!PV.IsMine)
			return;
		Move();
	}
	void Move()
	{	
		if(controlType == ControlType.PC)
		{
			moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}
		else if(controlType == ControlType.Android)
		{
			moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);
		}
		moveVelocity = moveInput.normalized * speed;
	}
	void FixedUpdate()
	{
		rb.MovePosition(rb.position + moveVelocity * Time.fixedDeltaTime);
	}

		public void TakeDamage(int damage)
	{
		PV.RPC(nameof(RPC_TakeDamage), PV.Owner, damage);
	}

	[PunRPC]
	void RPC_TakeDamage(int damage, PhotonMessageInfo info)
	{
		currentHealth -= damage;

		healthDisplay.text = "HP: " + currentHealth;

		if(currentHealth <= 0)
		{
			Die();
			//PlayerManager.Find(info.Sender).GetKill();
		}
	}

	void Die()
	{
		playerManager.Die();
	}
}
