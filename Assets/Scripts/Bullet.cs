using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	public float speed;
	public float lifeTime;
	public float distance;
	public int damage;
	public LayerMask whatIsSolid;

	private void Start()
	{
		Invoke("DestroyBullet", lifeTime);
	}
	private void Update() 
	{
		RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,transform.up, distance, whatIsSolid);
		if (hitInfo.collider != null)
		{
			if(hitInfo.collider.CompareTag("Player"))
			{
				Debug.Log("Get damage");
			}
			DestroyBullet();
		}
		transform.Translate(Vector2.up * speed * Time.deltaTime);
	}

	public void DestroyBullet()
	{
		Destroy(gameObject);
	}
}
