using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Screens : MonoBehaviour {

	public TextMeshPro text;
	public Timer timer;

	void Update(){
		text.SetText (timer.nowTime);
	}
}
