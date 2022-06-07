using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	GameObject spawn;
	GameObject spawn1;
	

	void Awake()
	{
		if (Instance)       //checks if another GameManager exists
		{
			Destroy(gameObject);    //there can only be one
			return;
		}
		DontDestroyOnLoad(gameObject);  // i am the only one..
		Instance = this;
		spawn = GameObject.Find("SpawnPoint");
		spawn1 = GameObject.Find("SpawnPoint1");
		Debug.Log("test");
		CreatePhoster();

	}


	void Start()
	{
	
	}

	void CreatePhoster()
	{

		/*Transform spawnpoint = SpawnManager.Instance.GetSpawnpoint();*/
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Phoster"), spawn.transform.position, spawn.transform.rotation,0);
		PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Screen"), spawn1.transform.position, spawn1.transform.rotation, 0);
	}
	//Quaternion.identity


}