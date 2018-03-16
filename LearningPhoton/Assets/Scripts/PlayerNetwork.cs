using UnityEngine;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork m_instance;
	public string m_playerName { get; private set; }

	void Awake() {
		m_instance = this;

		m_playerName = "Josh#" + Random.Range(1000, 9999);
	}
}
