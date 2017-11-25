using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CursorBehaviour : MonoBehaviour {

	public Camera cam;
	public float rotateSpeed;
	RaycastHit hit;
	public Image gaze;
	int layerMask = 1 << 8;

	public List<GameObject> cards = new List<GameObject>();

	public MeshRenderer blackout;
	bool isBlackout = false;
	public Transform startPos;
	public Timer timer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0, Input.GetAxis ("Mouse X") * rotateSpeed, 0);
		cam.transform.Rotate (-Input.GetAxis ("Mouse Y") * rotateSpeed, 0, 0);
		Debug.DrawRay (cam.transform.position, cam.transform.forward);
		if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, layerMask, QueryTriggerInteraction.Ignore)){
//			Debug.Log(hit.collider.name);
			if (hit.collider.gameObject.tag == "Point") {
				Grow temp = hit.collider.gameObject.GetComponent<Grow>();
				temp.canGrow = true;
			}
			if ((hit.collider.gameObject.tag == "Point") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					Vector3 newPos = hit.collider.transform.position;

					transform.position = new Vector3(newPos.x, 0, newPos.z);
					//				transform.rotation = hit.collider.transform.rotation;
					gaze.fillAmount = 0;
				}
			} else if ((hit.collider.gameObject.tag == "Card") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					cards.Add (hit.collider.gameObject);
					hit.collider.gameObject.SetActive (false);
					gaze.fillAmount = 0;
				}
			} else if ((hit.collider.gameObject.tag == "Button") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					Debug.Log ("RESET TIME!");
					gaze.fillAmount = 0;
					hit.collider.GetComponent<ResetTimer> ().ResetTime ();
					StartCoroutine ("Respawn", startPos);
				}
			} else if ((hit.collider.gameObject.tag == "Letter") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					gaze.fillAmount = 0;
					hit.collider.GetComponent<ReadLetter> ().PickToRead();
				}
			} else if ((hit.collider.gameObject.tag == "Continue") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					gaze.fillAmount = 0;
					hit.collider.GetComponent<FinishRead> ().FinishReading();
				}
			} else if ((hit.collider.gameObject.tag == "FinishPoint") && (gaze.fillAmount <= 1)) {
				gaze.fillAmount += 0.03f;
				if (gaze.fillAmount > 0.99f) {
					Vector3 newPos = hit.collider.transform.position;
					transform.position = new Vector3(newPos.x, 0, newPos.z);
					hit.collider.GetComponent<ToFinish> ().TeleportToFinish ();
					gaze.fillAmount = 0;
				}
			}
		} else {
			gaze.fillAmount -= 0.03f;
		}
	}

	public IEnumerator Respawn(Transform destiny){
		blackout.enabled = true;
		Color alpha = blackout.material.color;
		alpha.a += 0.1f;
		blackout.material.color = alpha;
		while (blackout.material.color.a > 0f) {
			if (!isBlackout) {
				alpha.a += 0.01f;
				blackout.material.color = alpha;
			} else {
				alpha.a -= 0.01f;
				blackout.material.color = alpha;
			}
			if (blackout.material.color.a >= 1f) {
				transform.position = destiny.position;
				transform.rotation = destiny.rotation;
				isBlackout = true;
				timer.StartCountdown ();
			}
			yield return new WaitForSeconds (Time.deltaTime);
		}
		blackout.enabled = false;
		alpha.a = 0.001f;
		blackout.material.color = alpha;
		isBlackout = false;
	}

}
