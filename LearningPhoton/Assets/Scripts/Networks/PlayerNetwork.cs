using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerNetwork : MonoBehaviour {

	public static PlayerNetwork m_instance;
	public string m_playerName { get; private set; }

	private PhotonView m_photonView;
	private int m_playersInGame = 0;

	void Awake() {
		m_instance = this;
		m_photonView = GetComponent<PhotonView>();
		m_playerName = "Josh#" + Random.Range(1000, 9999);

		SceneManager.sceneLoaded += OnSceneFinishLoading;
	}

	private void OnSceneFinishLoading(Scene scene, LoadSceneMode mode) {
		if(scene.name == "Game") {
			if(PhotonNetwork.isMasterClient) {
				MasterLoadedGame();
			} else {
				NonMasterLoadedGame();
			}
		}
	}

	private void MasterLoadedGame() {
		m_playersInGame = 1;
		m_photonView.RPC("RPCLoadGameOthers", PhotonTargets.Others);
	}

	private void NonMasterLoadedGame() {
		m_photonView.RPC("RPCLoadedGameScene", PhotonTargets.MasterClient);
	}

	[PunRPC]
	private void RPCLoadGameOthers() {
		PhotonNetwork.LoadLevel(1);
	}

	[PunRPC]
	private void RPCLoadedGameScene() {
		m_playersInGame++;
		if(m_playersInGame == PhotonNetwork.playerList.Length) {
			print("All players are in the game scene.");
		}
	}

	// make a function that resets the m_playersInGame if i want to reset the game lobby
}
