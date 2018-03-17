using System.Collections.Generic;
using UnityEngine;

public class RoomLayoutGroup : MonoBehaviour {

	[SerializeField]
	private GameObject m_roomListingPrefab;
	private GameObject m_getRoomListingPrefab {
		get { return m_roomListingPrefab; }
	}

	private List<RoomListing> m_roomListingButtons = new List<RoomListing>();
	private List<RoomListing> m_getRoomListingButtons {
		get { return m_roomListingButtons; }
	}

	private void OnReceivedRoomListUpdate() {
		RoomInfo[] rooms = PhotonNetwork.GetRoomList();

		foreach(RoomInfo room in rooms) {
			RoomReceived(room);
		}

		RemoveOldRooms();
	}

	private void RoomReceived(RoomInfo room) {
		int index = m_roomListingButtons.FindIndex(x => x.m_roomName == room.Name);
		if(index == -1) {
			if(room.IsVisible && room.PlayerCount < room.MaxPlayers) {
				GameObject roomListingObject = Instantiate(m_getRoomListingPrefab);
				roomListingObject.transform.SetParent(transform, false);

				RoomListing roomListing = roomListingObject.GetComponent<RoomListing>();
				m_getRoomListingButtons.Add(roomListing);

				index = (m_getRoomListingButtons.Count - 1);
			}
		}

		if(index != -1) {
			RoomListing roomListing = m_getRoomListingButtons[index];
			roomListing.SetRoomNameText(room.Name);
			roomListing.m_updated = true;
		}
	}

	private void RemoveOldRooms() {
		List<RoomListing> removeRooms = new List<RoomListing>();

		foreach(RoomListing roomListing in m_getRoomListingButtons) {
			if(!roomListing.m_updated) {
				removeRooms.Add(roomListing);
			} else {
				roomListing.m_updated = false;
			}
		}

		foreach(RoomListing roomListing in removeRooms) { 
			GameObject roomListingObject = roomListing.gameObject;
			m_getRoomListingButtons.Remove(roomListing);
			Destroy(roomListingObject);
		}
	}
}
