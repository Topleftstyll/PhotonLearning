using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvasManager : MonoBehaviour {

	public static MainCanvasManager m_instance;

	[SerializeField]
	private LobbyCanvas m_lobbyCanvas;
	public LobbyCanvas m_getLobbyCanvas {
		get { return m_lobbyCanvas; }
	}

	[SerializeField]
	private CurrentRoomCanvas m_currentRoomCanvas;
	public CurrentRoomCanvas m_getCurrentRoomCanvas {
		get { return m_currentRoomCanvas; }
	}

	void Awake() {
		m_instance = this;
	}
}
