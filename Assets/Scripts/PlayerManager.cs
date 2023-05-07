using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Linq;
using System.IO;

public class PlayerManager : MonoBehaviour
{
	PhotonView PV;
    GameObject controller;
	void Awake()
	{
		PV = GetComponent<PhotonView>();	
	}
    void Start()
    {
        if(PV.IsMine)
		{
			CreateController();
		}
    }

    
    void CreateController()
	{
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs","PlayerController"), Vector2.zero, Quaternion.identity);
	}
	public void Die()
	{
		PhotonNetwork.Destroy(controller);
	}
	
	public static PlayerManager Find(Player player)
	{
		return FindObjectsOfType<PlayerManager>().SingleOrDefault(x => x.PV.Owner == player);
	}
}
