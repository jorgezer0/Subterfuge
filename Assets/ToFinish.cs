using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToFinish : MonoBehaviour {

	public Transform endPoint;
	public CursorBehaviour player;

	public void TeleportToFinish(){
		player.StartCoroutine ("Respawn", endPoint);
	}
}
