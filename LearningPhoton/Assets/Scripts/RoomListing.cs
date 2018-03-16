using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {

	[SerializeField]
	private Text m_roomNameText;
	private Text m_getRoomNameText {
		get { return m_roomNameText; }
	}

	public string m_roomName { get; private set; }
	public bool m_updated { get; set;}

	void Start() {
		GameObject lobbyCanvasObject = MainCanvasManager.m_instance.m_getLobbyCanvas.gameObject;
		if(lobbyCanvasObject == null) {
			return;
		}

		LobbyCanvas lobbyCanvas = lobbyCanvasObject.GetComponent<LobbyCanvas>();
		Button button = GetComponent<Button>();
		button.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(m_getRoomNameText.text));
	}

	private void OnDestroy() {
		Button button = GetComponent<Button>();
		button.onClick.RemoveAllListeners();
	}

	public void SetRoomNameText(string text) {
		m_roomName = text;
		m_getRoomNameText.text = m_roomName;
	}
}
