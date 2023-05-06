using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public ControlType controlType;
	public Joystick joystick;

	public float speed;
	public int health;

	public enum ControlType{PC,Android};

	private Rigidbody2D rb;
	private Vector2 moveInput;
	private Vector2 moveVelocity;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
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

	public void ChangeHealth(int healthValue)
	{
		health += healthValue;
	}
}
