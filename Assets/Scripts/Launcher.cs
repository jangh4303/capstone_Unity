using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using System.IO;



public class Launcher : MonoBehaviourPunCallbacks
{
	[SerializeField] TMP_InputField roomNameInputField;
	[SerializeField] TMP_Text errorText;
	[SerializeField] TMP_Text roomNameText;
	[SerializeField] Transform roomListContent;
	[SerializeField] GameObject roomListItemPrefab;
	[SerializeField] Transform playerListContent;
	[SerializeField] GameObject PlayerListItemPrefab;
	[SerializeField] GameObject startGameButton;


	public static Launcher Instance;

	
	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		MenuManager.Instance.OpenMenu("loading");
		
		PhotonNetwork.ConnectUsingSettings();
	}

	public override void OnConnectedToMaster()
	{
		
		PhotonNetwork.JoinLobby();
		PhotonNetwork.AutomaticallySyncScene = true;
	}

	public override void OnJoinedLobby()
	{
		MenuManager.Instance.OpenMenu("StartMenu");
	
		PhotonNetwork.NickName = "Player " + Random.Range(0, 1000).ToString("0000");
	}

	public void CreateRoom()
	{
		if (string.IsNullOrEmpty(roomNameInputField.text))
		{
			return;
		}
		PhotonNetwork.CreateRoom(roomNameInputField.text);		//방생성
		MenuManager.Instance.OpenMenu("loading");
	}

	public override void OnCreateRoomFailed(short returnCode, string message)
	{
		errorText.text = "Room Creation Failed: " + message;
		
		MenuManager.Instance.OpenMenu("error");
	}

	/*  렌덤 방 입장
		public void JoinRandom()
		{
			PhotonNetwork.JoinRandomRoom();
		}
	*/
	public override void OnJoinedRoom()
	{
		 
		MenuManager.Instance.OpenMenu("room");
		roomNameText.text = PhotonNetwork.CurrentRoom.Name; 
		// 플레이어 리스트
		Player[] players = PhotonNetwork.PlayerList;

		foreach (Transform child in playerListContent)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i < players.Count(); i++)
		{
			Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
		}

		 startGameButton.SetActive(PhotonNetwork.IsMasterClient);  // 스타트 버튼 방장만 보임
	}

	public override void OnMasterClientSwitched(Player newMasterClient)
	{
		startGameButton.SetActive(PhotonNetwork.IsMasterClient);
	}

	

	public void StartGame()
	{
		PhotonNetwork.LoadLevel(1);
	}

	public void LeaveRoom()
	{
		PhotonNetwork.LeaveRoom();
		MenuManager.Instance.OpenMenu("loading");
	}

	public void JoinRoom(RoomInfo info)
	{
		PhotonNetwork.JoinRoom(info.Name);
		MenuManager.Instance.OpenMenu("loading");
	}



	public override void OnRoomListUpdate(List<RoomInfo> roomList)
	{
		foreach (Transform trans in roomListContent)
		{
			Destroy(trans.gameObject);
		}

		for (int i = 0; i < roomList.Count; i++)
		{
			if (roomList[i].RemovedFromList)
				continue;
			Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
		}
	}
	
	public override void OnPlayerEnteredRoom(Player newPlayer)
	{
		Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer);
	}
}