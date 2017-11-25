using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour {

	public CursorBehaviour player;
	public float time;
	float m, s;
	public string nowTime;
	bool countdown = true;

	public Transform startPos;

	// Use this for initialization
	void Start () {
		m = time-1;
		s = 60;
	}
	
	// Update is called once per frame
	void Update () {
		if (countdown) {
			if (m >= 0) {
				if (s > 0) {
					s -= Time.deltaTime;
					if (s <= 0) {
						m--;
						s = 60f;
						if (m < 0) {
							m = 0;
							s = 0;
							StartCoroutine ("ResetTime");
							player.StartCoroutine ("Respawn");
						}
					}
					if (s >= 10) {
						nowTime = (m + ":" + (int)s);
					} else {
						nowTime = (m + ":0" + (int)s);
					}
				}
			}
		}
	}

	public void StartCountdown(){
		countdown = true;
	}

	void StopCountdown(){
		countdown = false;
	}

	public IEnumerator ResetTime(){
		StopCountdown ();
		m = time-1;
		s = 60;
		yield return null;
	}
}
