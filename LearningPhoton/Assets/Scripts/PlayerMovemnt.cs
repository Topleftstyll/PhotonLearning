using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovemnt : Photon.MonoBehaviour {

	private PhotonView m_photonView;
	private Vector3 m_targetPosition;
	private Quaternion m_targetRotation;

	void Awake() {
		m_photonView = GetComponent<PhotonView>();
	}

	void Update() {
		if(m_photonView.isMine) {
			CheckInput();
		} else {
			SmoothMove();
		}
	}

	private void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
		if(stream.isWriting) {
			stream.SendNext(transform.position);
			stream.SendNext(transform.rotation);
		} else {
			m_targetPosition = (Vector3)stream.ReceiveNext();
			m_targetRotation = (Quaternion)stream.ReceiveNext();
		}
	}

	private void SmoothMove() {
		transform.position = Vector3.Lerp(transform.position, m_targetPosition, 0.25f);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, m_targetRotation, 500 * Time.deltaTime);
	}

	private void CheckInput() {
		float moveSpeed = 100.0f;
		float rotateSpeed = 500.0f;

		float vertical = Input.GetAxis("Vertical");
		float horizontal = Input.GetAxis("Horizontal");

		transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
		transform.Rotate(new Vector3(0, horizontal * rotateSpeed * Time.deltaTime, 0));
	}
}
