using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour {

	public int id;
	Animation anim;

	bool isOpen = false;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animation> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider col){
//		Debug.Log ("OnDoor");
		if (col.tag == "Player") {
			if (col.GetComponent<CursorBehaviour> ().cards.Count >= id) {
				if (!isOpen) {
					anim.Play ("OpenDoor");
					isOpen = true;
				}
			}
		}
	}

	void OnTriggerExit(Collider col){
		if (isOpen) {
			anim.Play ("CloseDoor");
			isOpen = false;
		}
	}
}
