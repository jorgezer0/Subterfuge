using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetTimer : MonoBehaviour {

	public Timer timer;

	public void ResetTime(){
		timer.StartCoroutine("ResetTime");
	}
}
