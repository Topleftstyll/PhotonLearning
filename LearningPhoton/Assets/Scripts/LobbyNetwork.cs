﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyNetwork : MonoBehaviour {

	void Start() {
		print("Connecting to sever...");
		PhotonNetwork.ConnectUsingSettings("0.0.0");
	}

	private void OnConnectedToMaster() {
		print("Connected to master...");
		PhotonNetwork.playerName = PlayerNetwork.m_instance.m_playerName;

		PhotonNetwork.JoinLobby(TypedLobby.Default);
	}

	private void OnJoinedLobby() {
		print("Joined Lobby..");
	}
}
