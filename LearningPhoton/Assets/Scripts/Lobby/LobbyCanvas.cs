using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

	[SerializeField]
	private RoomLayoutGroup m_roomLayoutGroup;
	private RoomLayoutGroup m_getRoomLayoutGroup {
		get { return m_roomLayoutGroup; }
	}

	public void OnClickJoinRoom(string roomName) {
		if(PhotonNetwork.JoinRoom(roomName)) {
			
		} else {
			print("Join room failed.");
		}
	}
}
