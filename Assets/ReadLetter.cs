using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadLetter : MonoBehaviour {

	public Vector3 startPos;
	public Vector3 startRot;
	public Transform readPos;
	Vector3 speed = Vector3.zero;
	public bool canRead = false;
	public bool finishing = false;
	MeshCollider col;

	// Use this for initialization
	void Start () {
		startPos = transform.localPosition;
		startRot = transform.rotation.eulerAngles;
		col = GetComponent<MeshCollider> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (canRead) {
			transform.position = Vector3.SmoothDamp (transform.position, readPos.position, ref speed, 0.5f);
			Debug.Log (transform.up);
			transform.LookAt (Camera.main.transform.position, Vector3.left);
			if ((transform.position - readPos.position).magnitude < 0.1) {
				canRead = false;
			}
		}
		if (finishing) {
			transform.position = Vector3.SmoothDamp (transform.position, startPos, ref speed, 0.6f);
			transform.rotation = Quaternion.Euler(Vector3.SmoothDamp (transform.rotation.eulerAngles, startRot, ref speed, 0.03f));
			if (transform.position == startPos) {
				finishing = false;

			}
		}
	}

	public void PickToRead(){
		canRead = true;
		col.enabled = false;
	}

	public void FinishReading(){
		this.gameObject.SetActive (false);
		finishing = true;
	}
}
