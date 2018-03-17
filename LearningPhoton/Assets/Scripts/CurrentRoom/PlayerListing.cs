using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour {

	public PhotonPlayer m_photonPlayer { get; private set; }

	[SerializeField]
	private Text m_playerName;
	private Text m_getPlayerName {
		get { return m_playerName; }
	}

	public void ApplyPhotonPlayer(PhotonPlayer photonPlayer) {
		m_photonPlayer = photonPlayer;
		m_getPlayerName.text = photonPlayer.NickName;
	}
}
