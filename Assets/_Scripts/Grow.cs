using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grow : MonoBehaviour {

	Vector3 newScale;
	float factor = 0.1f;
	public bool canGrow = false;

	void Update(){
		if (canGrow) {
			if (transform.localScale.x < 0.2f) {
				factor += 0.01f;
				newScale = new Vector3 (factor, factor, factor);
				transform.localScale = newScale;
			}
		} else {
			if (transform.localScale.x > 0.1f) {
				factor -= 0.02f;
				newScale = new Vector3 (factor, factor, factor);
				transform.localScale = newScale;
			}
		}
		canGrow = false;
	}

	void SelectedGrow(){
		canGrow = true;
	}
}
