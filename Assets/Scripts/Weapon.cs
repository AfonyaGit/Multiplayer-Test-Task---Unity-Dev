using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Weapon : MonoBehaviour
{
	public float offset;
	public GameObject bullet;
	public Transform shotPoint;

	private float timeBtwShots;
	public float startTimeBtwShots;
	private Vector3 difference;
	private float rotZ;
	private PlayerController player;
	public Joystick joystick;
	
	PhotonView PV;
	void Awake()
	{
		PV = GetComponent<PhotonView>();
	}

	private void Start()
	{
		if (!PV.IsMine)
			return;
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		
	}
	void Update()
	{
		if (!PV.IsMine)
			return;
		if(player.controlType == PlayerController.ControlType.PC)
		{
			difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
			rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
		}
		else if (player.controlType == PlayerController.ControlType.Android && Mathf.Abs(joystick.Horizontal) > 0.3f || Mathf.Abs(joystick.Vertical) > 0.3f)
		{
			
			rotZ = Mathf.Atan2( joystick.Vertical,joystick.Horizontal) * Mathf.Rad2Deg;

		}

		transform.rotation = Quaternion.Euler(0f, offset, rotZ + offset);

		if (timeBtwShots <= 0)
		{
			if (Input.GetMouseButton(0) && player.controlType== PlayerController.ControlType.PC)
			{
				Shoot();
			}
			else if (player.controlType == PlayerController.ControlType.Android)
			{
				if (joystick.Vertical != 0 || joystick.Horizontal != 0)
				Shoot();
			}
		}
		else
		{
			timeBtwShots -= Time.deltaTime;
		}
	}
	public void Shoot()
	{
		Instantiate(bullet, shotPoint.position, transform.rotation);
		timeBtwShots = startTimeBtwShots;
	}
}
