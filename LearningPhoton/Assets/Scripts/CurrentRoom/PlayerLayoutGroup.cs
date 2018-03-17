using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLayoutGroup : MonoBehaviour {

	[SerializeField]
	private GameObject m_playerListingPrefab;
	private GameObject m_getPlayerListingPrefab {
		get { return m_playerListingPrefab; }
	}

	private List<PlayerListing> m_playerListings = new List<PlayerListing>();
	private List<PlayerListing> m_getPlayerListings {
		get { return m_playerListings; }
	}

	// called by photon whenever the master client is switched
	private void OnMasterClientSwitched(PhotonPlayer newMasterClient) {
		PhotonNetwork.LeaveRoom();
	}

	// called by photon whenever you join a room
	private void OnJoinedRoom() {
		MainCanvasManager.m_instance.m_getCurrentRoomCanvas.transform.SetAsLastSibling();

		PhotonPlayer[] photonPlayers = PhotonNetwork.playerList;
		for(int i = 0; i < photonPlayers.Length; i++) {
			PlayerJoinedRoom(photonPlayers[i]);
		}
	}

	// called by photon whenever a player connects
	private void OnPhotonPlayerConnected(PhotonPlayer photonPlayer) {
		PlayerJoinedRoom(photonPlayer);
	}

	// called by photon whenever a player disconnects
	private void OnPhotonPlayerDisconnected(PhotonPlayer photonPlayer) {
		PlayerLeftRoom(photonPlayer);
	}

	private void PlayerJoinedRoom(PhotonPlayer photonPlayer) {
		if(photonPlayer == null) {
			return;
		}

		PlayerLeftRoom(photonPlayer);
		GameObject playerListingObject = Instantiate(m_getPlayerListingPrefab);
		playerListingObject.transform.SetParent(transform, false);

		PlayerListing playerListing = playerListingObject.GetComponent<PlayerListing>();
		playerListing.ApplyPhotonPlayer(photonPlayer);

		m_getPlayerListings.Add(playerListing);
	}

	private void PlayerLeftRoom(PhotonPlayer photonPlayer) {
		int index = m_getPlayerListings.FindIndex(x => x.m_photonPlayer == photonPlayer);
		if(index != -1) {
			Destroy(m_getPlayerListings[index].gameObject);
			m_getPlayerListings.RemoveAt(index);
		}
	}

	public void OnClickRoomState() {
		if(PhotonNetwork.isMasterClient) {
			PhotonNetwork.room.IsOpen = !PhotonNetwork.room.IsOpen;
			PhotonNetwork.room.IsVisible = PhotonNetwork.room.IsOpen;
		}
	}

	public void OnClickLeaveRoom() {
		PhotonNetwork.LeaveRoom();
	}
}
