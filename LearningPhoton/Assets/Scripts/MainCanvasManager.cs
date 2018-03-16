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

	void Awake() {
		m_instance = this;
	}
}
